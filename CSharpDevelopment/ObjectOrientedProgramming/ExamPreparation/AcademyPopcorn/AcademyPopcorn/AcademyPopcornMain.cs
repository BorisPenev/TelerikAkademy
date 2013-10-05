using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyPopcorn
{
    class AcademyPopcornMain
    {
        const int WorldRows = 23;
        const int WorldCols = 40;
        const int RacketLength = 6;

        static void Initialize(Engine engine)
        {
            int startRow = 3;
            int startCol = 2;
            int endCol = WorldCols - 2;

            //The AcademyPopcorn class contains an IndestructibleBlock class. Use it to create side and ceiling walls to the game. You can ONLY edit the AcademyPopcornMain.cs file
            IndestructibleBlock indestructibleBlock = null; 
            for (int row = 0; row < WorldRows; row++)
            {
                for (int col = 0; col < WorldCols; col++)
                {
                    if (row == 0)
                    {
                        indestructibleBlock = new IndestructibleBlock(new MatrixCoords(row, col));
                        engine.AddObject(indestructibleBlock);
                    }
                    else if (col == 0 || col == WorldCols - 1)
                    {
                        indestructibleBlock = new IndestructibleBlock(new MatrixCoords(row, col));
                        engine.AddObject(indestructibleBlock);
                    }
                }
            }

            for (int i = startCol; i < endCol; i++)
            {
                switch (i % 5)
                {
                    case 0:
                        ExplodingBlock explodingBlock = new ExplodingBlock(new MatrixCoords(startRow, i));
                        engine.AddObject(explodingBlock);
                        break;
                    default:
                        switch (i % 3)
                        {
                            case 0:
                                GiftBlock giftBlock = new GiftBlock(new MatrixCoords(startRow, i));
                                engine.AddObject(giftBlock);
                                break;
                            default:
                                Block currBlock = new Block(new MatrixCoords(startRow, i));
                                engine.AddObject(currBlock);
                                break;
                        }
                        break;
                }

                
            }

            for (int i = 4; i < endCol; i+=3)
            {
                UnpassableBlock currBlock = new UnpassableBlock(new MatrixCoords(8, i));

                engine.AddObject(currBlock);
            }

            TrailObject to = new TrailObject(new MatrixCoords(5, 6), 15);
            engine.AddObject(to);

            UnstoppableBall theBall = new UnstoppableBall(new MatrixCoords(WorldRows / 2, 0),
                new MatrixCoords(-1, 1));

            engine.AddObject(theBall);

            ShootingRacket theRacket = new ShootingRacket(new MatrixCoords(WorldRows - 1, WorldCols / 2), RacketLength);

            engine.AddObject(theRacket);
        }

        static void Main(string[] args)
        {
            IRenderer renderer = new ConsoleRenderer(WorldRows, WorldCols);
            IUserInterface keyboard = new KeyboardInterface();

            ShootEngine gameEngine = new ShootEngine(renderer, keyboard, 500);

            keyboard.OnLeftPressed += (sender, eventInfo) =>
            {
                gameEngine.MovePlayerRacketLeft();
            };

            keyboard.OnRightPressed += (sender, eventInfo) =>
            {
                gameEngine.MovePlayerRacketRight();
            };

            keyboard.OnActionPressed += (sender, eventInfo) =>
            {
                gameEngine.ShootPlayerRacket();
            };

            Initialize(gameEngine);

            //

            gameEngine.Run();
        }

    }
}
