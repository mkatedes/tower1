using System.Collections.Generic;
using tower1.Class;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace tower1.Load
{
    static class Enemies
    {
        public static Dictionary<string, List<Texture2D>> _enemyStandAnimations = new Dictionary<string, List<Texture2D>>();
        public static Dictionary<string, List<Texture2D>> _enemyWalkAnimations = new Dictionary<string, List<Texture2D>>();
        public static Dictionary<string, List<Texture2D>> _enemyDeathAnimations = new Dictionary<string, List<Texture2D>>();
        public static Dictionary<string, List<Texture2D>> _enemyAttackAnimations = new Dictionary<string, List<Texture2D>>();
        public static void LoadEnemies(ContentManager _content)
        {
            List<Texture2D> scorpionWalkTextures = new List<Texture2D>();
            List<Texture2D> scorpionStandTextures = new List<Texture2D>();
            List<Texture2D> scorpionAttackTextures = new List<Texture2D>();
            List<Texture2D> scorpionDieTextures = new List<Texture2D>();

            for (int i = 0; i < 19; i++)
            {
                scorpionStandTextures.Add(_content.Load<Texture2D>("enemy_1_idle_" + i));
            }
            _enemyStandAnimations.Add("scorpion", scorpionStandTextures);
            for (int i = 0; i < 19; i++)
            {
                scorpionWalkTextures.Add(_content.Load<Texture2D>("enemy_1_run_" + i));
            }
            _enemyWalkAnimations.Add("scorpion", scorpionWalkTextures);
            for (int i = 0; i < 19; i++)
            {
                scorpionDieTextures.Add(_content.Load<Texture2D>("enemy_1_die_" + i));
            }
            _enemyDeathAnimations.Add("scorpion", scorpionDieTextures);
            for (int i = 0; i < 19; i++)
            {
                scorpionAttackTextures.Add(_content.Load<Texture2D>("enemy_1_attack_" + i));
            }
            _enemyAttackAnimations.Add("scorpion", scorpionAttackTextures);
        }
    }
}
