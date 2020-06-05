# Sonar - Configuração Local

1. Instalar [JDK](https://www.oracle.com/java/technologies/javase-downloads.html)
2. Configurar variável de ambiente **JAVA_HOME**
3. Substituir conteúdo das variáveis do arquivo **sonar_lint.cake**
    * **{PROJECT_KEY}** nome do projeto analisado: [Sonar](https://sonarcloud.io)
    * **{SONAR_KEY}** token gerado no perfil da conta: [Sonar Security](https://sonarcloud.io/account/security)
    * Por padrão a organização está configurada como *wizdevops*
        * Para alterar caso necessário: [Sonar Organizations](https://sonarcloud.io/account/organizations)
4. Executar terminal (Powershell ou CMD) como **Administrador**
5. Executar script

* Powershell

> *.\sonar_lint.ps1* 

* Shell

> *sh sonar_lint.sh*