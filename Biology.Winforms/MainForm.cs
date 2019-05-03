namespace Biology.Winforms
{
    using Biology.Core;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class MainForm : Form
    {
        private static readonly IReadOnlyDictionary<CreatureType, Color> Colors = new Dictionary<CreatureType, Color>
        {
            {CreatureType.Blue, Color.Blue},
            {CreatureType.Green, Color.Green},
            {CreatureType.Red, Color.Red},
            {CreatureType.Orange, Color.Orange},
        };

        private readonly IEnumerator<IReadOnlyDictionary<CreatureType, int>> enumerator;

        private int generation = 0;

        private CancellationTokenSource cancellationSource;

        public MainForm()
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
                    .WithCrowdingCoefficient(0.001)
                    .WithReplicationProbabilityPerCreature(0.1)
                    .Build(),
            };

            var populationHistoryDistribution = new InfinitePopulationHistoryDistribution(creatures);

            this.enumerator = populationHistoryDistribution.Sample().GetEnumerator();
        }

        private async void PlayButton_Click(object sender, EventArgs e)
        {
            this.playButton.Enabled = false;
            this.pauseButton.Enabled = true;

            this.cancellationSource = new CancellationTokenSource();

            while (this.enumerator.MoveNext())
            {
                this.generationsLabel.Text = $"Gen: {this.generation}";

                this.DisplayChart(this.enumerator.Current);

                if (this.cancellationSource.IsCancellationRequested)
                {
                    break;
                }

                await Task.Delay(50);

                this.generation++;
            }
        }

        private void DisplayChart(IReadOnlyDictionary<CreatureType, int> populations)
        {
            var series = this.chart.Series[0];
            series.Points.Clear();

            foreach (var keyValuePair in populations)
            {
                var creatureType = keyValuePair.Key;
                var population = keyValuePair.Value;

                var index = series.Points.AddXY(creatureType.ToString(), population);
                series.Points[index].Color = Colors[creatureType];
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            this.playButton.Enabled = true;
            this.pauseButton.Enabled = false;

            this.cancellationSource.Cancel();
        }
    }
}
