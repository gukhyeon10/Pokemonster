
namespace PokemonSpace
{
    enum ObjectEnum
    {
        TILE = 0,
        BUILD = 1,
        ITEM = 2,
        NPC = 3,
        GOODS = 4,
        NATURE = 5,
        BUSH = 6,
    }

    enum SkillType
    {
        NORMAL = 0,
        GRASS = 1, 
        WATER = 2,
        FIRE = 3,
        ELECTRICITY = 4,
        ICE = 5,
        FIGHT = 6,
        FLYING = 7,
        POISON = 8,
        BUG = 9,
        EVIL = 10,
        ESPER = 11,
        DRAGON = 12,
        ROCK = 13,
        GROUND = 14,
        GHOST = 15,
    }

    enum ItemType
    {
        TOOL = 0,
        RECOVERY =1,
        BALL = 2,
        TECH = 3,
        FRUIT = 4,
    }

    enum SkillClass
    {
        PHYSICS = 0,
        SPECIAL = 1,
        CONDITION = 2,
    }

    enum AbilityKind
    {
        ATTACK =0,
        DEFENCE =1,
        SPECIAL_ATTACK =2,
        SPECIAL_DEFENCE = 3,
    }
    public class FilePath
    {
        public const string MapFolderPath = "D:/Guk/Unity/Pokemon/Assets/Resources/Map/";
        public const string MapBushFolderPath = "D:/Guk/Unity/Pokemon/Assets/Resources/MapBush/";
        public const string MapNpcFolderPath = "D:/Guk/Unity/Pokemon/Assets/Resources/MapNpc/";
        public const string PokemonDataPath = "D:/Guk/Unity/Pokemon/Assets/Resources/Pokedex/";

    }

    public struct MapData
    {
        public int tileNumber, tileRotate;
        public string tileCode, buildCode, npcCode;
        public bool movable;

        public MapData(int tileNumber, string tileCode, int tileRotate, string buildCode, string npcCode, bool isTileMovable)
        {
            this.tileNumber= tileNumber;
            this.tileCode = tileCode;
            this.tileRotate = tileRotate;
            this.buildCode = buildCode;
            this.npcCode = npcCode;
            this.movable = isTileMovable;
        }
    }

    public struct BushData
    {
        public int bushTileNumber;
        public string bushCode;
        public int bushRotate;
        public BushData(int tileNumber, string bushCode, int bushRotate)
        {
            this.bushTileNumber = tileNumber;
            this.bushCode = bushCode;
            this.bushRotate = bushRotate;
        }
    }

    public struct NpcData
    {
        public int npcTileNumber;
        public string itemNumber, itemDialog, dialog;
        public bool isMoveOn, isFightOn;
    }

    public struct PokedexData
    {
        public string no;
        public string name;
        public string kind;
        public string height;
        public string weight;
        public string detail;

        public void DataSet(string no, string name, string kind, string height, string weight, string detail)
        {
            this.no = no;
            this.name = name;
            this.kind = kind;
            this.height = height;
            this.weight = weight;
            this.detail = detail;

        }
    }

    public struct TribeData
    {
        public string no;
        public string name;
        public string hp;
        public string attack;
        public string defence;
        public string sp_attack;
        public string sp_defence;
        public string speed;

        public TribeData(string no, string name, string hp, string attack, string defence, string sp_attack, string sp_defence, string speed)
        {
            this.no = no;
            this.name = name;
            this.hp = hp;
            this.attack = attack;
            this.defence = defence;
            this.sp_attack = sp_attack;
            this.sp_defence = sp_defence;
            this.speed = speed;
        }
    }

    public struct PokemonData
    {
        public string no;
        public string name;
        public int level;
        public int attack;
        public int defence;
        public int specialAttack;
        public int specialDefence;
        public int speed;
        public int remainHp;
        public int maxHp;

        public int skill_one;
        public int skill_one_pp;
        public int skill_two;
        public int skill_two_pp;
        public int skill_three;
        public int skill_three_pp;
        public int skill_four;
        public int skill_four_pp;

        public PokemonData(string no, string name, int level, int attack, int defence, int specialAttack, int specialDefence, int speed, int remainHp, int maxHp)
        {
            this.no = no;
            this.name = name;
            this.level = level;
            this.attack = attack;
            this.defence = defence;
            this.specialAttack = specialAttack;
            this.specialDefence = specialDefence;
            this.speed = speed;
            this.remainHp = remainHp;
            this.maxHp = maxHp;

            this.skill_one = 0;
            this.skill_one_pp = 0;
            this.skill_two = 0;
            this.skill_two_pp = 0;
            this.skill_three = 0;
            this.skill_three_pp = 0;
            this.skill_four = 0;
            this.skill_four_pp = 0;
        }
        
    }

    public struct Item
    {
        public int no;
        public int type;
        public string name;
        public string text;

        public Item(int no, int type, string name, string text)
        {
            this.no = no;
            this.type = type;
            this.name = name;
            this.text = text;
        }
    }

    public struct Skill
    {
        public int no;
        public string name;
        public int skill_Type;
        public int skill_Class;
        public int power;
        public int hitPercent;
        public int pp;

        public Skill(int no, string name, int skill_Type, int skill_Class, int power, int hitPercent, int pp)
        {
            this.no = no;
            this.name = name;
            this.skill_Type = skill_Type;
            this.skill_Class = skill_Class;
            this.power = power;
            this.hitPercent = hitPercent;
            this.pp = pp;
        }
    }
}
