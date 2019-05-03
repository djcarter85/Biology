namespace Biology.Winforms
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Biology.Core;

    public partial class Form1 : Form
    {
        private readonly IEnumerator<IReadOnlyDictionary<CreatureType, int>> enumerator;

        public Form1()
        {
            this.InitializeComponent();

            var creatures = new List<Creature>
            {
                CreatureBuilder.Create(CreatureType.Blue)
                    .WithSpontaneousBirthProbability(1)
                    .WithDeathProbabilityPerCreature(0.1)
                    .WithReplicationProbabilityPerCreature(0.05)
                    .WithMutationProbability(CreatureType.Green, 0.1)
                    .WithMutationProbability(CreatureType.Red, 0.1)
                    .Build(),
                CreatureBuilder.Create(CreatureType.Green)
                    .WithSpontaneousBirthProbability(0)
                    .WithDeathProbabilityPerCreature(0.1)
                    .WithReplicationProbabilityPerCreature(0.05)
                    .Build(),
                CreatureBuilder.Create(CreatureType.Red)
                    .WithSpontaneousBirthProbability(0)
                    .WithDeathProbabilityPerCreature(0.05)
                    .WithReplicationProbabilityPerCreature(0.05)
                    .WithMutationProbability(CreatureType.Orange, 0.05)
                    .Build(),
                CreatureBuilder.Create(CreatureType.Orange)
                    .WithSpontaneousBirthProbability(0)
                    .WithDeathProbabilityPerCreature(0.05)
                    .WithReplicationProbabilityPerCreature(0.1)
                    .Build(),
            };

            var populationHistoryDistribution = new InfinitePopulationHistoryDistribution(creatures);

            this.enumerator = populationHistoryDistribution.Sample().GetEnumerator();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            this.enumerator.MoveNext();

            var populations = this.enumerator.Current;

            this.label1.Text = string.Join(", ", populations.Select(kvp => $"{kvp.Key}: {kvp.Value:00}"));
        }
    }
}
