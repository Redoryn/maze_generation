using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity_ScratchPad.MazeAlgorithms;

namespace Unity_ScratchPad
{


    public partial class Form1 : Form
    {
        Random r;
        int Rows = 30;
        int Columns = 40;
        int TileSize = 16;
        int frameCount = 120;
        List<AlgorithmListItem> listOfAlgos;

        public Form1()
        {
            InitializeComponent();
            r = new Random(DateTime.Now.Millisecond);
            listOfAlgos = new List<AlgorithmListItem>()
            {
                new AlgorithmListItem()
                {
                    Name = "Rec. Backtrack",
                    Algo = new RecursiveBacktracker()
                },
                new AlgorithmListItem()
                {
                    Name = "Prims",
                    Algo = new RandomizedPrims()
                }
            };
        }

        private Color RandomColor()
        {
            return Color.FromArgb(r.Next(255), r.Next(255), r.Next(255));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateMaze();
        }


        private MazeController mazeController;

        private void CreateMaze()
        {
            mazeController = new MazeController(picBoxGrid.CreateGraphics());
            var item = listBoxAlgorithms.SelectedItem;
            if (item == null) return;
            AlgorithmListItem algoListItem = item as AlgorithmListItem;
            mazeController.SetAlgorithm(algoListItem.Algo);
            mazeController.Initialize(Rows, Columns, TileSize);
            mazeController.Draw();
        }

        private void btnStepForward_Click(object sender, EventArgs e)
        {
            mazeController.StepForward(5);
            mazeController.Draw();
        }



        private void FillWithRandomTiles()
        {
            Graphics formGraphics = this.CreateGraphics();

            using (ColoredGrid grid = new ColoredGrid(Rows, Columns, TileSize, Color.Black, formGraphics))
            {
                for (int i = 0; i < 100; i++)
                {
                    Color c = RandomColor();
                    grid.Set(r.Next(0, Rows), r.Next(0, Columns), c);
                }

                grid.Draw(formGraphics);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mazeController != null)
            {
                mazeController.Dispose();
            }
        }

        private void btnRunToCompletion_Click(object sender, EventArgs e)
        {
            mazeController.RunToCompletion();
            mazeController.Draw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBoxAlgorithms.DataSource = listOfAlgos;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            mazeController.Reset();
            mazeController.Draw();
        }
    }

    public class AlgorithmListItem
    {
        public string Name { get; set; }
        public IMazeGeneratorAlgorithm Algo;

        public override string ToString()
        {
            return Name;
        }
    }
}
