using System.Windows.Input;
using System.Collections.Generic;
using EntityControl;
using System;
using System.Windows.Media;

namespace CollisionTesting
{
    class MasterLevelControls
    {
        Rocket player;
        Entity[] terrain;
        Rocket_NPC[] enemyRockets;
        Launcher[] launchers;
        int terrainCollidesTo;
        readonly string FilePath;
        public bool GameWin = false;
        Key userCommand;
        public MasterLevelControls(Rocket p, Entity[] t, Launcher[] l, Rocket_NPC[] r, int tcol, Key uc) //player, terrain, terraincollide, usercommand
        {
            player = p;
            terrain = t;
            terrainCollidesTo = tcol;
            userCommand = uc;
            launchers = l;
            enemyRockets = r;
        }

        public string[] AppendToLabel()
        {
            List<string> strings = new List<string>();
            for (int x = 0; x < enemyRockets.Length; x++)
            {
                strings.Add(enemyRockets[x].Angle.ToString() + " " + launchers[x].IsLineOfSight(player.character.Margin.Left, player.character.Margin.Top, terrain[0]) + "\n"); //missile angle and if turret can see
            }
            return strings.ToArray();
        }

        public MasterLevelControls(Key uc)
        {
            userCommand = uc;
        }

        public MasterLevelControls(string path)
        {
            FilePath = path + ".alpr";//probably unused file ext
        }

        public bool[] IsCollision(Rocket player, Entity terrain)
        {
            double compareValue; //really not needed, but it helps me read the code easier
            bool padHit = false; //indicates is the last value was a landing pad
            bool flagX = false; //return true, padHit if flagX or flagY are true
            bool flagY = false; //returns false if else.
            #region declare comaprison variables
            double ph = player.character.Height; //player height from collisions
            double pl = player.character.Margin.Left; //player left
            double pt = player.character.Margin.Top; //player top
            double pw = player.character.Width; //player width
            double th = terrain.character.Height; //terrain height
            double tl = terrain.character.Margin.Left; //terrain left
            double tt = terrain.character.Margin.Top; //terrain top
            double tw = terrain.character.Width; //terrain width
            #endregion
            #region get specific height/width using trig
            ph -= Math.Abs((player.character.Height - player.character.Width) * Math.Sin(Math.PI * player.Angle / 180)); //ranges from default height (ph - 0) to default width (ph - ph + pw)
            pw += Math.Abs((player.character.Height - player.character.Width) * Math.Sin(Math.PI * player.Angle / 180)); //same code for width (pw + 0) -> (pw - pw + ph)
            #endregion
            if (pl < tl)
            {
                compareValue = pl + pw; //sets side to compare to right side. 
            }
            else
            {
                compareValue = pl; //same, but it checks the left side
            }
            if (tl < compareValue && compareValue < tl + tw)
            {
                padHit = terrain.IsLandingPad; //since it's the last value, should be ok to override each time
                flagX = true;
            } //checks if the side is in between the two parts of the object - a y-axis collision
            else if ((tl < pl && pl + pw < tl + tw) || (pl < tl && tl + tw < pl + pw))
            {
                padHit = terrain.IsLandingPad;
                flagX = true;
            }
            if (pt < tt)
            {
                compareValue = pt + ph; //sets side to compare to top side. 
            }
            else
            {
                compareValue = pt; //check bottom
            }
            if (tt < compareValue && compareValue < tt + th)
            {
                flagY = true;
            }
            else if ((tt < pt && pt + ph < tt + th) || (pt < tt && tt + th < pt + ph))
            {
                padHit = terrain.IsLandingPad;
                flagY = true;
            }
            //this code is essentially the same as its x-axis counterpart, so it was copy/pasted
            if (flagX && flagY)
            {
                return new bool[2] { true, padHit };
            }
            else { return new bool[2] { false, false }; };
        } //returns WasCollision, WasPad

        public bool IsCollision(Rocket player, Rocket_NPC terrain)
        {
            double compareValue; //really not needed, but it helps me read the code easier
            bool padHit = false; //indicates is the last value was a landing pad
            bool flagX = false; //return true, padHit if flagX or flagY are true
            bool flagY = false; //returns false if else.
            #region declare comaprison variables
            double ph = player.character.Height; //player height from collisions
            double pl = player.character.Margin.Left; //player left
            double pt = player.character.Margin.Top; //player top
            double pw = player.character.Width; //player width
            double th = terrain.character.Height; //terrain height
            double tl = terrain.character.Margin.Left; //terrain left
            double tt = terrain.character.Margin.Top; //terrain top
            double tw = terrain.character.Width; //terrain width
            #endregion
            #region get specific height/width using trig
            ph -= Math.Abs((player.character.Height - player.character.Width) * Math.Sin(Math.PI * player.Angle / 180)); //ranges from default height (ph - 0) to default width (ph - ph + pw)
            pw += Math.Abs((player.character.Height - player.character.Width) * Math.Sin(Math.PI * player.Angle / 180)); //same code for width (pw + 0) -> (pw - pw + ph)
            th -= Math.Abs((terrain.character.Height - terrain.character.Width) * Math.Sin(Math.PI * terrain.Angle / 180));//next lines are repeats but for enemy rockets
            tw += Math.Abs((terrain.character.Height - terrain.character.Width) * Math.Sin(Math.PI * terrain.Angle / 180)); 
            #endregion
            if (pl < tl)
            {
                compareValue = pl + pw; //sets side to compare to right side. 
            }
            else
            {
                compareValue = pl; //same, but it checks the left side
            }
            if (tl < compareValue && compareValue < tl + tw)
            {
                padHit = terrain.IsLandingPad; //since it's the last value, should be ok to override each time
                flagX = true;
            } //checks if the side is in between the two parts of the object - a y-axis collision
            else if ((tl < pl && pl + pw < tl + tw) || (pl < tl && tl + tw < pl + pw))
            {
                padHit = terrain.IsLandingPad;
                flagX = true;
            }
            if (pt < tt)
            {
                compareValue = pt + ph; //sets side to compare to top side. 
            }
            else
            {
                compareValue = pt; //check bottom
            }
            if (tt < compareValue && compareValue < tt + th)
            {
                flagY = true;
            }
            else if ((tt < pt && pt + ph < tt + th) || (pt < tt && tt + th < pt + ph))
            {
                flagY = true;
            }
            //this code is essentially the same as its x-axis counterpart, so it was copy/pasted
            if (flagX && flagY)
            {
                return true;
            }
            else { return false; }
        } //same thing as its bool[] counterpart, but calculates between player and npc

        public bool StandardTimerAlgorithm() //returns true if player is alive, false if dead
        {
            #region User Commands
            bool Quitting = false;
            Key key = userCommand;
            if (key == Key.Up)
            {
                player.Thrust();
            }
            else if (key == Key.Right)
            {
                player.Rotate(5);
            }
            else if (key == Key.Left)
            {
                player.Rotate(0 - 5);
            }
            else if (key == Key.Escape)
            {
                Quitting = true;
            }
            #endregion
            //Update
            player.UniversalUpdate();
            #region Missiles
            if (enemyRockets.Length > 0)
            {
                #region check if missiles can fire
                for (int x = 0; x < launchers.Length; x++)
                {
                    bool[] checkfire = new bool[terrain.Length];
                    for (int y = 0; y < terrain.Length; y++)
                    {
                        checkfire[y] = launchers[x].IsLineOfSight(player.character.Margin.Left, player.character.Margin.Top, terrain[y]);
                    }
                    launchers[x].FireMissile = true;
                    for (int y = 0; y < checkfire.Length; y++)
                    {
                        if (!checkfire[y]) { launchers[x].FireMissile = false; break; }
                    }
                    launchers[x].ChildAlive = enemyRockets[x].IsAlive; //controls who can fire
                    launchers[x].CheckFire(); //this sets it to false if missile not ready
                }
                #endregion
                #region fire lauchable missiles
                for (int x = 0; x < launchers.Length; x++)
                {
                    if (launchers[x].FireMissile)
                    {
                        enemyRockets[x].Spawning();
                    }
                }
                #endregion
            }
            #region rockets move
            for (int x = 0; x < enemyRockets.Length; x++)
            {
                enemyRockets[x].UpdateAppearance();
                if (enemyRockets[x].IsAlive)
                {
                    enemyRockets[x].Thrust();
                    enemyRockets[x].RotateToPlayer((int)player.character.Margin.Left, (int)player.character.Margin.Top);
                }
            }
            #endregion
            #region launchers and rockets update
            for (int x = 0; x < launchers.Length; x++)
            {
                launchers[x].NextFrame();
                enemyRockets[x].UniversalUpdate();
            }
            #endregion
            #endregion
            return CheckDeath(Quitting);
        }

        public Key KeyRelease(object sender, KeyEventArgs e)
        {
            if (e.Key == userCommand)
            { return userCommand = Key.DbeEnterWordRegisterMode; } //this has to be the most useless key ever - assigning it to essentially "undo" keypress
            else { return userCommand; }
        }
        public Key KeyPress(object sender, KeyEventArgs e)
        {
            return e.Key;
        }

        public void InitialiseLevel(ref int terrainCol, ref Entity[] terrainOut, ref Launcher[] launchersOut, ref Rocket_NPC[] rocketsOut, ref Rocket player, ref int lastTerrain, ref int EnemyCol)
        {
            List<string> args = new List<string>(); //length depends on level, but a list can store them all
            #region pull data from file
            System.IO.StreamReader reader = new System.IO.StreamReader(@"bin\levels\" + FilePath);
            while (!reader.EndOfStream)
            {
                args.Add(reader.ReadLine());
            } //adds lines until EOS
            reader.Close();
            #endregion
            #region read level meta
            terrainCol = args.Count - 2;
            string[] metadata = args[0].Split(",".ToCharArray()[0]); //split can't take string, have to use ->char[], 0 index instead
            player.GravityPower = System.Convert.ToSingle(metadata[0]); //thrust and gravity are stored as singles
            player.ThrustPower = System.Convert.ToSingle(metadata[1]); //so they have to be converted using system.convert

            #endregion
            //skip level scores
            #region read terrain data
            List<Entity> terrain = new List<Entity>();
            List<Launcher> launchers = new List<Launcher>();
            List<Rocket_NPC> rockets = new List<Rocket_NPC>();
            for (int x = 0; x < args.Count - 2; x++)
            {
                string[] terrainPart = args[x + 2].Split(",".ToCharArray()[0]); //split into width/height/coords(x)/coords(y) in indexes 0,1,2,3
                if (terrainPart[4] == "E") //run if the entity is an enemy
                {
                    terrainCol--; EnemyCol++;
                    launchers.Add(new Launcher(int.Parse(terrainPart[0]), int.Parse(terrainPart[1]), int.Parse(terrainPart[2])));
                    rockets.Add(new Rocket_NPC(int.Parse(terrainPart[0]) + 20, int.Parse(terrainPart[1]) - 25, player.GravityPower, player.ThrustPower, 10)); //very similar to player, but npc that autotracks
                }
                else if (terrainPart[4] == "T") //run if the entity is terrain
                {
                    terrain.Add(new Entity(int.Parse(terrainPart[0]), int.Parse(terrainPart[1]), int.Parse(terrainPart[2]), int.Parse(terrainPart[3])));
                    lastTerrain = terrain.Count; //add terrain using parsed->int file args
                }
            }
            launchersOut = launchers.ToArray();
            terrainOut = terrain.ToArray();
            rocketsOut = rockets.ToArray();
            #endregion
        }

        public string[] ReadHighScores()
        {
            string baseData;
            System.IO.StreamReader reader = new System.IO.StreamReader(FilePath);
            reader.ReadLine(); //discards the first line
            baseData = reader.ReadLine(); //for reference, the exact syntax of the file is as follows
                                          //s rank time, a rank time, b rank time, s ranks, a ranks, b ranks, c ranks
            reader.Close();
            return baseData.Split(",".ToCharArray()[0]); //split to array by comma
        } //returns array with highscore times and high scores

        public bool CheckDeath(bool Quitting)
        {
            #region Check for user death
            //check out of bounds
            if (player.character.Margin.Left < 0 || player.character.Margin.Top < 0 || player.character.Margin.Left > 765 || player.character.Margin.Top > 385) //the window is ~ this size, with a small leeway to account for rotation
            {
                player.DeathFlag(3);
                return false;
            }
            //check for quit
            if (Quitting)
            {
                player.DeathFlag(0);
                return false;
            }
            #region check for missile hits
            if (enemyRockets.Length > 0)
            {
                for (int x = 0; x < enemyRockets.Length; x++)
                {
                    if (IsCollision(player, enemyRockets[x]))
                    {
                        player.DeathFlag(1);
                        return false;
                    }
                }
            }
            #endregion
            #region check for terrain collisions
            for (int x = 0; x < terrainCollidesTo; x++)
            {
                bool[] result = IsCollision(player, terrain[x]);
                if (result[0])
                {
                    if (result[1])
                    {
                        player.DeathFlag(4);
                        GameWin = true;
                        return false;
                    }
                    else
                    {
                        player.DeathFlag(2);
                        return false;
                    }
                }
            }
            #endregion
            #endregion
            #region Check for Rocket deaths
            #region check for terrain collisions
            for (int y = 0; y < enemyRockets.Length; y++)
            {
                if (enemyRockets[y].IsAlive)
                {
                    //check out of bounds
                    if (enemyRockets[y].character.Margin.Left < 0 || enemyRockets[y].character.Margin.Top < 0 || enemyRockets[y].character.Margin.Left > 765 || enemyRockets[y].character.Margin.Top > 385)
                    {
                        enemyRockets[y].Death();
                    }
                    for (int x = 0; x < terrainCollidesTo; x++)
                    {
                        if (IsCollision(enemyRockets[y], terrain[x])[0])
                        {
                            enemyRockets[y].Death();
                        }
                    }
                }
            }
            #endregion
            #endregion
            return true;
        }
    }
}