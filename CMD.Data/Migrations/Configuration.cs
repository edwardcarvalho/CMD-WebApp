namespace CMD.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using CMD.Model.Models;
    using CMD.Shared.Helpers;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.EntityContext.EfContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.EntityContext.EfContext context)
        {



            context.Cargo.AddOrUpdate(c => c.Descricao, new Cargo() { Descricao = "T.I" }); //1
            context.Cargo.AddOrUpdate(c => c.Descricao, new Cargo() { Descricao = "Atendente I" }); //2
            context.Cargo.AddOrUpdate(c => c.Descricao, new Cargo() { Descricao = "Gerente" }); //3
            context.Cargo.AddOrUpdate(c => c.Descricao, new Cargo() { Descricao = "Supervisor" }); //4
            context.Cargo.AddOrUpdate(c => c.Descricao, new Cargo() { Descricao = "Coordenador" }); //5
            context.SaveChanges();

            context.Filial.AddOrUpdate(c => c.Nome, new Filial() { Nome = "Alphaview", Ativo = true });
            context.SaveChanges();

            context.Operacao.AddOrUpdate(c => c.OperacaoId, new Operacao()
            {
                OperacaoId = 1,
                Nome = "Operacao Teste"
            });
            context.SaveChanges();

            context.Funcionario.AddOrUpdate(c => c.FuncionarioId, new Funcionario()
            {
                FuncionarioId = 1,
                Ativo = true,
                CargoId = context.Cargo.Where(c => c.CargoId == 1).First().CargoId, //T.I
                Cpf = "312.122.688-65",
                FilialId = context.Filial.Where(c => c.FilialId == 1).First().FilialId,
                Matricula = "CMD2018",
                //Medidas = new List<Medidas>(),
                Nome = "Edward Carvalho",
                OperacaoId = context.Operacao.Where(c => c.OperacaoId == 1).First().OperacaoId,
                Rg = "40.634.084-5",
                Email = "edward.silva@iteris.com.br"
            });

            context.Funcionario.AddOrUpdate(c => c.FuncionarioId, new Funcionario()
            {
                FuncionarioId = 2,
                Ativo = true,
                CargoId = context.Cargo.Where(c => c.CargoId == 5).First().CargoId, //Coordenador
                Cpf = "111.111.111-11",
                FilialId = context.Filial.Where(c => c.FilialId == 1).First().FilialId,
                Matricula = "CMD2019",
                //Medidas = new List<Medidas>(),
                Nome = "Usuário Coordenador",
                OperacaoId = context.Operacao.Where(c => c.OperacaoId == 1).First().OperacaoId,
                Rg = "1111111111",
                Email = "edward.silva@iteris.com.br"
            });

            context.Funcionario.AddOrUpdate(c => c.FuncionarioId, new Funcionario()
            {
                FuncionarioId = 3,
                Ativo = true,
                CargoId = context.Cargo.Where(c => c.CargoId == 3).First().CargoId, //Gerente
                Cpf = "222.222.222-22",
                FilialId = context.Filial.Where(c => c.FilialId == 1).First().FilialId,
                Matricula = "CMD2020",
                //Medidas = new List<Medidas>(),
                Nome = "Usuário Gerente",
                OperacaoId = context.Operacao.Where(c => c.OperacaoId == 1).First().OperacaoId,
                Rg = "2222222222",
                Email = "edward.silva@iteris.com.br"
            });

            context.Funcionario.AddOrUpdate(c => c.FuncionarioId, new Funcionario()
            {
                FuncionarioId = 4,
                Ativo = true,
                CargoId = context.Cargo.Where(c => c.CargoId == 3).First().CargoId, //RH / Gerente
                Cpf = "333.333.333-33",
                FilialId = context.Filial.Where(c => c.FilialId == 1).First().FilialId,
                Matricula = "CMD2021",
                //Medidas = new List<Medidas>(),
                Nome = "Usuário RH",
                OperacaoId = context.Operacao.Where(c => c.OperacaoId == 1).First().OperacaoId,
                Rg = "3333333333",
                Email = "edward.silva@iteris.com.br"
            });

            context.Funcionario.AddOrUpdate(c => c.FuncionarioId, new Funcionario()
            {
                FuncionarioId = 5,
                Ativo = true,
                CargoId = context.Cargo.Where(c => c.CargoId == 2).First().CargoId, //RH / Gerente
                Cpf = "444.444.444-44",
                FilialId = context.Filial.Where(c => c.FilialId == 1).First().FilialId,
                Matricula = "CMD2022",
                //Medidas = new List<Medidas>(),
                Nome = "Rafael Rabelo",
                OperacaoId = context.Operacao.Where(c => c.OperacaoId == 1).First().OperacaoId,
                Rg = "4444444444",
                Email = "edward.silva@iteris.com.br"
            });
            context.SaveChanges();

            context.Operacao.AddOrUpdate(c => c.OperacaoId, new Operacao()
            {
                OperacaoId = 1,
                Nome = "Operacao Teste",
                SupervisorId = context.Funcionario.Where(c => c.FuncionarioId == 2).First().FuncionarioId,
                GerenteId = context.Funcionario.Where(c => c.FuncionarioId == 3).First().FuncionarioId
            });
            context.SaveChanges();

            context.Perfil.AddOrUpdate(c => c.PerfilId, new Perfil() { PerfilId = 1, Descricao = "Administrador" }); //1
            context.Perfil.AddOrUpdate(c => c.PerfilId, new Perfil() { PerfilId = 2, Descricao = "Coordenador" }); //2
            context.Perfil.AddOrUpdate(c => c.PerfilId, new Perfil() { PerfilId = 3, Descricao = "Gerente" }); //3
            context.Perfil.AddOrUpdate(c => c.PerfilId, new Perfil() { PerfilId = 4, Descricao = "RH" }); //4
            context.SaveChanges();

            if (!context.Users.Any(u => u.UserName == "edward"))
            {
                var store = new UserStore<Usuarios>(context);
                var manager = new UserManager<Usuarios>(store);
                var user = new Usuarios()
                {
                    EmailConfirmed = true,
                    Ativo = true,
                    Email = "edward.carvalho@gmail.com",
                    UserName = "edward",
                    FuncionarioId = 1,
                    PerfilId = 1,
                    LockoutEnabled = false,
                };

                manager.Create(user, "It3r1509#");
            }
            if (!context.Users.Any(u => u.UserName == "coordenador"))
            {
                var store = new UserStore<Usuarios>(context);
                var manager = new UserManager<Usuarios>(store);
                var user = new Usuarios()
                {
                    EmailConfirmed = true,
                    Ativo = true,
                    UserName = "coordenador",
                    FuncionarioId = 2,
                    PerfilId = 2,
                    LockoutEnabled = false,
                };

                manager.Create(user, "coordenador");

            }
            if (!context.Users.Any(u => u.UserName == "gerente"))
            {
                var store = new UserStore<Usuarios>(context);
                var manager = new UserManager<Usuarios>(store);
                var user = new Usuarios()
                {
                    EmailConfirmed = true,
                    Ativo = true,
                    UserName = "gerente",
                    FuncionarioId = 3,
                    PerfilId = 3,
                    LockoutEnabled = false,
                };

                manager.Create(user, "gerente");
            }

            if (!context.Users.Any(u => u.UserName == "rh"))
            {
                var store = new UserStore<Usuarios>(context);
                var manager = new UserManager<Usuarios>(store);
                var user = new Usuarios()
                {
                    EmailConfirmed = true,
                    Ativo = true,
                    UserName = "rh",
                    FuncionarioId = 4,
                    PerfilId = 4,
                    LockoutEnabled = false,
                };

                manager.Create(user, "It3r1509#");
            }

            //context..AddOrUpdate(c => c.UsuarioId, new Usuarios() { UsuarioId = 1, Ativo = true, FuncionarioId = context.Funcionario.Where(c => c.FuncionarioId == 1).First().FuncionarioId, Login = "edward", Senha = Helper.EncryptPassword("It3r1509#"), PerfilId = 1});
            //context.Usuario.AddOrUpdate(c => c.UsuarioId, new Usuarios() { UsuarioId = 2, Ativo = true, FuncionarioId = context.Funcionario.Where(c => c.FuncionarioId == 2).First().FuncionarioId, Login = "coordenador", Senha = Helper.EncryptPassword("coordenador"), PerfilId = 2 });
            //context.Usuario.AddOrUpdate(c => c.UsuarioId, new Usuarios() { UsuarioId = 3, Ativo = true, FuncionarioId = context.Funcionario.Where(c => c.FuncionarioId == 3).First().FuncionarioId, Login = "gerente", Senha = Helper.EncryptPassword("gerente"), PerfilId = 3 });
            //context.Usuario.AddOrUpdate(c => c.UsuarioId, new Usuarios() { UsuarioId = 4, Ativo = true, FuncionarioId = context.Funcionario.Where(c => c.FuncionarioId == 4).First().FuncionarioId, Login = "rh", Senha = Helper.EncryptPassword("rh"), PerfilId = 4 });
            //context.SaveChanges();

            context.Advertencia.AddOrUpdate(c => c.Descricao, new Advertencias() { Ativo = true, Descricao = "Atendimento" });
            context.Advertencia.AddOrUpdate(c => c.Descricao, new Advertencias() { Ativo = true, Descricao = "Conduta" });
            context.Advertencia.AddOrUpdate(c => c.Descricao, new Advertencias() { Ativo = true, Descricao = "Falta" });
            context.Advertencia.AddOrUpdate(c => c.Descricao, new Advertencias() { Ativo = true, Descricao = "Ponto" });
            context.Advertencia.AddOrUpdate(c => c.Descricao, new Advertencias() { Ativo = true, Descricao = "Horário" });
            context.SaveChanges();

            context.Motivo.AddOrUpdate(c => c.Descricao, new Motivos() { Ativo = true, Descricao = "Falando ao celular em horário de trabalho" });
            context.Motivo.AddOrUpdate(c => c.Descricao, new Motivos() { Ativo = true, Descricao = "Desrespeitando o cliente ao telefone" });
            context.Motivo.AddOrUpdate(c => c.Descricao, new Motivos() { Ativo = true, Descricao = "Comendo no posto de trabalho" });
            context.Motivo.AddOrUpdate(c => c.Descricao, new Motivos() { Ativo = true, Descricao = "Falta de ética com o grupo de trabalho" });
            context.Motivo.AddOrUpdate(c => c.Descricao, new Motivos() { Ativo = true, Descricao = "Falta Injustificada" });
            context.SaveChanges();

            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Reprovada" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Aprovada" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Aprovação Coordenador/Gerente" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Aprovação RH" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Impressa" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Bloqueado (Perda do Prazo de Impressão)" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Bloqueado (Perda do Prazo de Aprovação - Gerente)" });
            context.StatusMedida.AddOrUpdate(c => c.Descricao, new StatusMedida() { Descricao = "Bloqueado (Perda do Prazo de Aprovação - RH)" });
            context.SaveChanges();
        }
    }
}
