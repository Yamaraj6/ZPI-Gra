using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models
{
    public class Card
    {
        public int CardVariant { get; set; } 

        public string SkillName { get; set; }
        public int SkillId { get; set; }
        public int SkillDmg { get; set; }
        public int CastingSpeed { get; set; }
        public int ManaUsage { get; set; }

        public string SkillColor { get; set; }


    }
}
