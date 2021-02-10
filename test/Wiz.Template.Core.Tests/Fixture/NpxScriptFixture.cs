using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Wiz.Template.Core.Tests.Fixture
{
    public class NpxScriptFixture : IDisposable
    {
        private static readonly Regex urls =
            new Regex(
                "(ht|f)tp(s?)\\:\\/\\/[0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*(:(0-9)*)*(\\/?)([a-zA-Z0-9\\-\\.\\?\\,\\'\\/\\\\\\+&%\\$#_]*)?",
                RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private Process process;
        public string Url { get; private set; }
        public bool HasServer => !string.IsNullOrEmpty(Url);

        public int ProcessId => process?.Id ?? 0;

        private readonly TaskCompletionSource<bool> signal = new TaskCompletionSource<bool>(false);

        /// <summary>
        /// Will execute a script name with default value of "dev"
        /// i.e. npx [tool name]
        /// </summary>
        public NpxScriptFixture()
        {

        }

        /// <summary>
        /// Will wait for npm to start up and retrieve the first url from the output.
        /// **Only use this to run the development server.**
        /// </summary>
        /// <param name="output"></param>
        /// <param name="timeout">In milliseconds</param>
        /// <returns></returns>
        public async Task RunAsync(string tool, string arguments, Action<string> output = null, int timeout = 2000)
        {
            if (string.IsNullOrWhiteSpace(tool))
            {
                throw new NullReferenceException("O parâmetro tool deve ser informado");
            }

            lock (signal)
            {
                if (process == null)
                {
                    var info = new ProcessStartInfo("cmd")
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        Arguments = $"/c start {tool} {arguments}",
                        UseShellExecute = false,
                        //WindowStyle = ProcessWindowStyle.Hidden,
                        //CreateNoWindow = true
                    };

                    process = Process.Start(info);
                    process.EnableRaisingEvents = true;
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    // Process the NPX output and attempt
                    // to find a URL. This will stop processing
                    // when it finds the first URL
                    process.OutputDataReceived += (sender, eventArgs) =>
                    {
                        output?.Invoke(eventArgs.Data);

                        if (!string.IsNullOrEmpty(eventArgs.Data) && string.IsNullOrEmpty(Url))
                        {
                            var results = urls.Matches(eventArgs.Data);

                            if (results.Any())
                            {
                                Url = results.First().Value;
                                signal.SetResult(true);
                            }
                        }
                    };

                    // Terrible things have happened
                    // so we can stop waiting for the success
                    // event to occur, because it ain't happening
                    process.ErrorDataReceived += (sender, args) =>
                    {
                        output?.Invoke(args.Data);

                        if (!signal.Task.IsCompleted)
                        {
                            signal.SetException(new Exception("npx failed to start"));
                        }
                    };

                    // set a timeout to wait for the process
                    // to finish starting and find the Url. If it doesn't then we
                    // assume that the user just ran a script
                    var cancellationTokenSource = new CancellationTokenSource(timeout);
                    cancellationTokenSource.Token.Register(() =>
                    {
                        if (signal.Task.IsCompleted)
                            return;

                        // we don't want to wait for a url anymore
                        Url = string.Empty;
                        signal.SetResult(true);
                    }, false);
                }
            }

            await signal.Task;
        }

        public void Dispose()
        {
            process?.Dispose();
        }
    }
}