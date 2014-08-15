#region Info/Author
//----------------------------------------------------------------------------
// Author:    Tazi Mehdi
// Source:    Chimera 2D GAMES ENGINE
// Info:      Score Manager
//-----------------------------------------------------------------------------
#endregion
#region Using Statement
using System;
using System.Collections.Generic;
#endregion
namespace Chimera.Game_Feature
{
#if !XBOX
    /// <summary>
    ///This Class Offer you the possibility to manage you score
    /// </summary>
    public class Score
    {
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public Score()
        {
            sortedscore = new int[0];
            sortedplayer = new string[0];
            score = new SortedDictionary<string, int>();
        }
        #region Fields & Properties
        private SortedDictionary<string, int> score;
        private string[] sortedplayer;
        private int[] sortedscore;

        /// <summary>
        /// Return All Players Sorted
        /// </summary>
        public string[] SortedPlayers
        {
            get { return sortedplayer; }
        }
        /// <summary>
        /// Return All Scores Sorted
        /// </summary>
        public int[] SortedScores
        {
            get { return sortedscore; }
        }
        #endregion

        #region Main Functions
        /// <summary>
        /// Add Score The To Manager
        /// </summary>
        /// <param name="Player">Player Name To Add</param>
        /// <param name="Score">Player Score To Add</param>
        /// <returns></returns>
        public bool Add(string Player,int Score)
        {
            try
            {
                score.Add(Player, Score);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Remove Score By The Player Name From The Manager
        /// </summary>
        /// <param name="Player"></param>
        /// <param name="Score"></param>
        public void Remove(string Player, int Score)
        {
                score.Remove(Player);
        }
        /// <summary>
        /// Remove All Scores From The Manager
        /// </summary>
        public void Clear()
        {
            sortedplayer = new string[0];
            sortedscore = new int[0];
            score.Clear();
        }
        /// <summary>
        /// Edit A Player Score From The Manager
        /// </summary>
        /// <param name="Player">Player To Edit</param>
        /// <param name="Score">The New Score</param>
        /// <returns></returns>
        public bool Edit(string Player,int Score)
        {
            try
            {
                score[Player] = Score;
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Get The Number Of Players In The Manager
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return score.Count;
        }
        /// <summary>
        /// Get The Number Of Players In The Manager
        /// </summary>
        /// <returns></returns>n
        public int GetLenght()
        {
            return score.Count;
        }
        #endregion
        // To Optimize
        #region Sorting Functions
        /// <summary>
        /// Sort The Score By Player Name
        /// </summary>
        public void SortedByPlayerName()
        {
            int i=0;
            sortedplayer = new string[score.Count];
            sortedscore = new int[score.Count];
            
            foreach(KeyValuePair<string,int> s in score)
            {
                sortedplayer[i] = s.Key;
                sortedscore[i] = s.Value;
                i++;
            }
            
        }
        /// <summary>
        /// Sort The Score By The Score Value
        /// </summary>
        public void SortedByScore()
        {
            SortedByPlayerName();
            SortedDictionary<int, string> s = new SortedDictionary<int, string>();
            int i;
            for (i = 0; i < score.Count; i++)
            {
                s.Add(sortedscore[i], sortedplayer[i]);
            }

            i = 0;

            foreach (KeyValuePair<int, string> ss in s)
            {
                sortedplayer[i] = ss.Value;
                sortedscore[i] = ss.Key;
                i++;
            }

        }//TO OPTIMIZE
        #endregion
    }
#endif
}
