using System;
using System.Data;
using System.Data.Common;
using Moq;

namespace Wiz.Template.Core.Tests.Mocks.Factory
{
    public class MockRepositoryBuilder
    {
        private static readonly Mock<DbConnection> _conn;

        static MockRepositoryBuilder()
        {
            _conn = new Mock<DbConnection>();
        }

        public static Mock<DbConnection> GetMockDbConnection()
        {
            return _conn;
        }
    }

}
