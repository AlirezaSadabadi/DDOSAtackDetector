using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Models.Migrations
{
    internal sealed class Configurations : DbMigrationsConfiguration<Models.DatabaseContext>
    {
        //متغیری برای چک کردن اینکه اگر اطلاعات از قبل وارد بانک شده
        //دوباره همان اطلاعات اولیه وارد نشود
        private readonly bool _pendingMigrations;
        public Configurations()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "DatabaseContext";
            AutomaticMigrationDataLossAllowed = true;
            //در این قسمت چک می گردد که آیا لازم است دوباره اطلاعات اولیه وارد بانک گردد
            var migrator = new DbMigrator(this);
            _pendingMigrations = migrator.GetPendingMigrations().Any();
        }

        /// <summary>
        /// این متد فقط بار اولی که بانک می خواهد ساخته شود صدا زده می شود
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(DatabaseContext context)
        {
            //در این قسمت چک می گردد که آیا لازم است دوباره اطلاعات اولیه وارد بانک گردد
            //دلیلش هم این است که متد سید هر دفعه اجرا شده و اطلاعات اولیه تکراری تولید می گردد
            //لازم به ذکر است که دفعه اولی که می خواهد بانک اجرا گردد بایستی این خط کامنت گردد
            if (!_pendingMigrations) return;

            base.Seed(context);

            //Fact
            var priorProbLearningCoefficient = new Fact() {Title= "PriorProbLearningCoefficient", Value="0.01"};
            var oldAttackLearningCoefficient = new Fact() { Title = "OldAttackLearningCoefficient", Value = "0.01" };
            var cptProbLearningCoefficeint = new Fact() { Title = "cptProbLearningCoefficeint", Value = "0.01" };
            var botnetTrail = new Fact() { Title = "BotNet", Property = "Trail", Value = "180000-350000" };
            var botnetDensity = new Fact() { Title = "BotNet", Property = "Density", Value = "NonUniform Distribution" };
            var botnetSize = new Fact() { Title = "BotNet", Property = "Size", Value = "Random" };
            var botnetDomain = new Fact() { Title = "BotNet", Property = "Domain", Value = "Variable" };
            var botnetDynamic = new Fact() { Title = "BotNet", Property = "Dynamic", Value = "Time Dependent" };
            var popularWebPageTraffic = new Fact() { Title = "PopularWebPageTraffic", Property = "Distribution", Value = "Zipf Distribution" };
            var webTraffic = new Fact() { Title = "WebTraffic", Property = "Law", Value = "Pareto Law" };

            //Facts
            context.Facts.AddOrUpdate(priorProbLearningCoefficient);
            context.Facts.AddOrUpdate(oldAttackLearningCoefficient);
            context.Facts.AddOrUpdate(cptProbLearningCoefficeint);
            context.Facts.AddOrUpdate(botnetTrail);
            context.Facts.AddOrUpdate(botnetDensity);
            context.Facts.AddOrUpdate(botnetSize);
            context.Facts.AddOrUpdate(botnetDomain);
            context.Facts.AddOrUpdate(botnetDynamic);
            context.Facts.AddOrUpdate(popularWebPageTraffic);
            context.Facts.AddOrUpdate(webTraffic);

            //SymptomAndAttack
            var onSign = new SymptomAndAttack() { Title = "OnSign", PriorProb = 0.5, SorA = "S", UI = true, Sorter=1};
            var iPFFN = new SymptomAndAttack() { Title = "IPFFN", PriorProb = 0.5, SorA = "S", UI = true, Sorter =2 };
            var dNSFFN = new SymptomAndAttack() { Title = "DNSFFN", PriorProb = 0.5, SorA = "S", UI = true, Sorter =3 };
            var noParto = new SymptomAndAttack() { Title = "NoParto", PriorProb = 0.5, SorA = "S", UI = true, Sorter =4};
            var noZipf = new SymptomAndAttack() { Title = "NoZipf", PriorProb = 0.5, SorA = "S", UI = true, Sorter =5};
            var flowEntropy = new SymptomAndAttack() { Title = "FlowEntropy", PriorProb = 0.5, SorA = "S", UI = true, Sorter =6};
            var correlationCoefficient = new SymptomAndAttack() { Title = "CorrelationCoefficient", SorA = "S", UI = false, Sorter =7};
            var oldAttack = new SymptomAndAttack() { Title = "OldAttack", PriorProb = 0.5, SorA = "S", UI = false, Sorter =8};
            var botNet = new SymptomAndAttack() { Title = "BotNet", SorA = "S", UI = false, Sorter =9};
            var floodingAttack = new SymptomAndAttack() { Title = "FloodingAttack", SorA = "A", UI = false, Sorter =10};

            //SymptomAndAttacks
            context.SymptomAndAttacks.AddOrUpdate(onSign);
            context.SymptomAndAttacks.AddOrUpdate(iPFFN);
            context.SymptomAndAttacks.AddOrUpdate(dNSFFN);
            context.SymptomAndAttacks.AddOrUpdate(noParto);
            context.SymptomAndAttacks.AddOrUpdate(noZipf);
            context.SymptomAndAttacks.AddOrUpdate(flowEntropy);
            context.SymptomAndAttacks.AddOrUpdate(correlationCoefficient);
            context.SymptomAndAttacks.AddOrUpdate(oldAttack);
            context.SymptomAndAttacks.AddOrUpdate(botNet);
            context.SymptomAndAttacks.AddOrUpdate(floodingAttack);

            //SymptomAndRelation
            var r1 = new SymptomAndAttackRelation() { SymptomAndAttack1 = onSign, SymptomAndAttack2 = botNet, Arrange = 1 };
            var r2 = new SymptomAndAttackRelation() { SymptomAndAttack1 = iPFFN, SymptomAndAttack2 = botNet, Arrange = 2 };
            var r3 = new SymptomAndAttackRelation() { SymptomAndAttack1 = dNSFFN, SymptomAndAttack2 = botNet, Arrange = 3 };
            var r4 = new SymptomAndAttackRelation() { SymptomAndAttack1 = noParto, SymptomAndAttack2 = botNet, Arrange = 4 };
            var r5 = new SymptomAndAttackRelation() { SymptomAndAttack1 = noZipf, SymptomAndAttack2 = botNet, Arrange = 5 };
            var r6 = new SymptomAndAttackRelation() { SymptomAndAttack1 = flowEntropy, SymptomAndAttack2 = correlationCoefficient, Arrange = 1 };
            var r7 = new SymptomAndAttackRelation() { SymptomAndAttack1 = flowEntropy, SymptomAndAttack2 = floodingAttack, Arrange = 1 };
            var r8 = new SymptomAndAttackRelation() { SymptomAndAttack1 = correlationCoefficient, SymptomAndAttack2 = floodingAttack, Arrange = 2 };
            var r9 = new SymptomAndAttackRelation() { SymptomAndAttack1 = oldAttack, SymptomAndAttack2 = floodingAttack, Arrange = 3 };
            var r10 = new SymptomAndAttackRelation() { SymptomAndAttack1 = botNet, SymptomAndAttack2 = floodingAttack, Arrange = 4 };

            //SymptomAndRelations
            context.SymptomAndAttackRelations.AddOrUpdate(r1);
            context.SymptomAndAttackRelations.AddOrUpdate(r2);
            context.SymptomAndAttackRelations.AddOrUpdate(r3);
            context.SymptomAndAttackRelations.AddOrUpdate(r4);
            context.SymptomAndAttackRelations.AddOrUpdate(r5);
            context.SymptomAndAttackRelations.AddOrUpdate(r6);
            context.SymptomAndAttackRelations.AddOrUpdate(r7);
            context.SymptomAndAttackRelations.AddOrUpdate(r8);
            context.SymptomAndAttackRelations.AddOrUpdate(r9);
            context.SymptomAndAttackRelations.AddOrUpdate(r10);

            //CPT
            //FloodingAttack
            var c1 = new CPT() { SymptomAndAttack = floodingAttack, State = 15, Prob = 1 };
            var c2 = new CPT() { SymptomAndAttack = floodingAttack, State = 14, Prob = 0.95 };
            var c3 = new CPT() { SymptomAndAttack = floodingAttack, State = 13, Prob = 0.9 };
            var c4 = new CPT() { SymptomAndAttack = floodingAttack, State = 12, Prob = 0.85 };
            var c5 = new CPT() { SymptomAndAttack = floodingAttack, State = 11, Prob = 0.9 };
            var c6 = new CPT() { SymptomAndAttack = floodingAttack, State = 10, Prob = 0.8 };
            var c7 = new CPT() { SymptomAndAttack = floodingAttack, State = 9, Prob = 0.6 };
            var c8 = new CPT() { SymptomAndAttack = floodingAttack, State = 8, Prob = 0.5 };
            var c9 = new CPT() { SymptomAndAttack = floodingAttack, State = 7, Prob = 0.6 };
            var c10 = new CPT() { SymptomAndAttack = floodingAttack, State = 6, Prob = 0.5 };
            var c11 = new CPT() { SymptomAndAttack = floodingAttack, State = 5, Prob = 0.1 };
            var c12 = new CPT() { SymptomAndAttack = floodingAttack, State = 4, Prob = 0 };
            var c13 = new CPT() { SymptomAndAttack = floodingAttack, State = 3, Prob = 0.6 };
            var c14 = new CPT() { SymptomAndAttack = floodingAttack, State = 2, Prob = 0.5 };
            var c15 = new CPT() { SymptomAndAttack = floodingAttack, State = 1, Prob = 0.1 };
            var c16 = new CPT() { SymptomAndAttack = floodingAttack, State = 0, Prob = 0 };
            //CorrelationCoefficient
            var c17 = new CPT() { SymptomAndAttack = correlationCoefficient, State = 1, Prob = 0.5 };
            var c18 = new CPT() { SymptomAndAttack = correlationCoefficient, State = 0, Prob = 0 };
            //BotNet
            var c19 = new CPT() { SymptomAndAttack = botNet, State = 31, Prob = 1 };
            var c20 = new CPT() { SymptomAndAttack = botNet, State = 30, Prob = 0.85 };
            var c21 = new CPT() { SymptomAndAttack = botNet, State = 29, Prob = 0.95 };
            var c22 = new CPT() { SymptomAndAttack = botNet, State = 28, Prob = 0.8 };
            var c23 = new CPT() { SymptomAndAttack = botNet, State = 27, Prob = 1 };
            var c24 = new CPT() { SymptomAndAttack = botNet, State = 26, Prob = 0.85 };
            var c25 = new CPT() { SymptomAndAttack = botNet, State = 25, Prob = 0.95 };
            var c26 = new CPT() { SymptomAndAttack = botNet, State = 24, Prob = 0.8 };
            var c27 = new CPT() { SymptomAndAttack = botNet, State = 23, Prob = 1 };
            var c28 = new CPT() { SymptomAndAttack = botNet, State = 22, Prob = 0.85 };
            var c29 = new CPT() { SymptomAndAttack = botNet, State = 21, Prob = 0.9 };
            var c30 = new CPT() { SymptomAndAttack = botNet, State = 20, Prob = 0.8 };
            var c31 = new CPT() { SymptomAndAttack = botNet, State = 19, Prob = 0.6 };
            var c32 = new CPT() { SymptomAndAttack = botNet, State = 18, Prob = 0.45 };
            var c33 = new CPT() { SymptomAndAttack = botNet, State = 17, Prob = 0.5 };
            var c34 = new CPT() { SymptomAndAttack = botNet, State = 16, Prob = 0.4 };
            var c35 = new CPT() { SymptomAndAttack = botNet, State = 15, Prob = 0.6 };
            var c36 = new CPT() { SymptomAndAttack = botNet, State = 14, Prob = 0.45 };
            var c37 = new CPT() { SymptomAndAttack = botNet, State = 13, Prob = 0.5 };
            var c38 = new CPT() { SymptomAndAttack = botNet, State = 12, Prob = 0.4 };
            var c39 = new CPT() { SymptomAndAttack = botNet, State = 11, Prob = 0.6 };
            var c40 = new CPT() { SymptomAndAttack = botNet, State = 10, Prob = 0.45 };
            var c41 = new CPT() { SymptomAndAttack = botNet, State = 9, Prob = 0.5 };
            var c42 = new CPT() { SymptomAndAttack = botNet, State = 8, Prob = 0.4 };
            var c43 = new CPT() { SymptomAndAttack = botNet, State = 7, Prob = 0.6 };
            var c44 = new CPT() { SymptomAndAttack = botNet, State = 6, Prob = 0.45 };
            var c45 = new CPT() { SymptomAndAttack = botNet, State = 5, Prob = 0.55 };
            var c46 = new CPT() { SymptomAndAttack = botNet, State = 4, Prob = 0.4 };
            var c47 = new CPT() { SymptomAndAttack = botNet, State = 3, Prob = 0.2 };
            var c48 = new CPT() { SymptomAndAttack = botNet, State = 2, Prob = 0.05 };
            var c49 = new CPT() { SymptomAndAttack = botNet, State = 1, Prob = 0.15 };
            var c50 = new CPT() { SymptomAndAttack = botNet, State = 0, Prob = 0 };

            //CPTs
            context.CPTs.AddOrUpdate(c1);
            context.CPTs.AddOrUpdate(c2);
            context.CPTs.AddOrUpdate(c3);
            context.CPTs.AddOrUpdate(c4);
            context.CPTs.AddOrUpdate(c5);
            context.CPTs.AddOrUpdate(c6);
            context.CPTs.AddOrUpdate(c7);
            context.CPTs.AddOrUpdate(c8);
            context.CPTs.AddOrUpdate(c9);
            context.CPTs.AddOrUpdate(c10);
            context.CPTs.AddOrUpdate(c11);
            context.CPTs.AddOrUpdate(c12);
            context.CPTs.AddOrUpdate(c13);
            context.CPTs.AddOrUpdate(c14);
            context.CPTs.AddOrUpdate(c15);
            context.CPTs.AddOrUpdate(c16);
            context.CPTs.AddOrUpdate(c17);
            context.CPTs.AddOrUpdate(c18);
            context.CPTs.AddOrUpdate(c19);
            context.CPTs.AddOrUpdate(c20);
            context.CPTs.AddOrUpdate(c21);
            context.CPTs.AddOrUpdate(c22);
            context.CPTs.AddOrUpdate(c23);
            context.CPTs.AddOrUpdate(c24);
            context.CPTs.AddOrUpdate(c25);
            context.CPTs.AddOrUpdate(c26);
            context.CPTs.AddOrUpdate(c27);
            context.CPTs.AddOrUpdate(c28);
            context.CPTs.AddOrUpdate(c29);
            context.CPTs.AddOrUpdate(c30);
            context.CPTs.AddOrUpdate(c31);
            context.CPTs.AddOrUpdate(c32);
            context.CPTs.AddOrUpdate(c33);
            context.CPTs.AddOrUpdate(c34);
            context.CPTs.AddOrUpdate(c35);
            context.CPTs.AddOrUpdate(c36);
            context.CPTs.AddOrUpdate(c37);
            context.CPTs.AddOrUpdate(c38);
            context.CPTs.AddOrUpdate(c39);
            context.CPTs.AddOrUpdate(c40);
            context.CPTs.AddOrUpdate(c41);
            context.CPTs.AddOrUpdate(c42);
            context.CPTs.AddOrUpdate(c43);
            context.CPTs.AddOrUpdate(c44);
            context.CPTs.AddOrUpdate(c45);
            context.CPTs.AddOrUpdate(c46);
            context.CPTs.AddOrUpdate(c47);
            context.CPTs.AddOrUpdate(c48);
            context.CPTs.AddOrUpdate(c49);
            context.CPTs.AddOrUpdate(c50);
        }

    }
}
