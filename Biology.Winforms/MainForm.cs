namespace Biology.Winforms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Windows.Forms.DataVisualization.Charting;
    using Biology.Core;

    public partial class MainForm : Form
    {
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
                series.Points.AddXY(keyValuePair.Key.ToString(), keyValuePair.Value);
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
