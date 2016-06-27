using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BBS.BBRZ;

namespace BBS.Models
{
    public class Modifier
    {
        public Modifier()
        {
            Value = 0;
        }

        public Modifier(DiceModifier dm)
        {
            Value = dm.Value;
            Skill = (SkillEnum)dm.Skill;
            Type = (ModifierType)dm.Type;
        }

        public int Value { get; protected set; }
        public SkillEnum Skill { get; protected set; }
        public ModifierType Type { get; protected set; }

        public override string ToString()
        {
            if (Skill == SkillEnum.NoSkill)
                return String.Format("{0}={1}", Type, Value.ToString("+0;-#"));
            else if (Skill == SkillEnum.Player)
                return String.Format("{0}:{1}={2}", Skill, Type, Value.ToString("+0;-#"));
            else
                return String.Format("{0}={1}", Skill, Value.ToString("+0;-#"));
        }
    }

    public enum ModifierType
    {
        Unknown = -1,
        unknown0 = 0,
        unknown1 = 1, 
        Dodge = 2, 
        TackleZone = 3, 
        AccuratePass = 4, 
        PickUp = 5, 
        FastPass = 6,
        ShortPass = 7,
        LongPass = 8, 
        LongBombPass = 9, 
        HandOff = 10,
        FriendlyBlockAssist = 11,
        EnemyBlockAssist = 12, 
        unknown13 = 13, 
        Interception = 14,
        unknown15 = 15,
        unknown16 = 16,
        unknown17 = 17,
        FriendlyAdvice = 18,
        HittingAnEnemy = 19,
        unknown20 = 20,
        BloodweiserBabes = 21,
        unknown22 = 22,
        unknown23 = 23,
        PlayerMovement = 24,
        ShadowerMovement = 25,
        unknown26 = 26,
        unknown27 = 27,
        unknown28 = 28,
        unknown29 = 29,
        unknown30 = 30,
        unknown31 = 31,
        unknown32 = 32,
    }
}
