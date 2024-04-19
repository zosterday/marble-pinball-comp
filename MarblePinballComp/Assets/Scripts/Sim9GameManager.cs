using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Sim9GameManager : MonoBehaviour
{
    private const float SpawnXBound = 2.36f;

    private const float SpawnYStart = 4.48f;

    private const float SpawnXOffset = 0.4f;

    private const float SpawnYOffset = 0.2f;

    private const float StartingMarbleSpawnBoxBound = 0.3f;

    private const float StartingMarbleSpawnBoxYOffset = 3.5f;

    private const float WinnerScaleFactor = 120f;

    private static readonly List<string> usernames = new() {
        "@uncrustable.memess",
        "@radioactivejelly17",
        "@fishy_hotdog",
        "@jschlattlover1999",
        "@sayyy.zae_",
        "@christian_m4",
        "@letmacylaydown",
        "@myt_robotic",
        "@obixodabatata",
        "@owenwoods2021",
        "@hamiltonhoodfails2005",
        "@real_manuelc",
        "@official_tizianog",
        "@teddyc1313",
        "@masterbaiter000",
        "@warrenfox537",
        "@bruce_the_goose24",
        "@not.straight._line",
        "@ags816710",
        "@sage_cryme",
        "@briansteelrfan",
        "@hellnaww.coms",
        "@lexsus_lfa_1063",
        "@b0undl3sss",
        "@e__rik03",
        "@mahjuobr",
        "@cdstone26",
        "@jfoltz25",
        "@azurethamafaka",
        "@bodinmax1",
        "@_couplehunnidbands",
        "@fugaku.lol",
        "@roman1213412",
        "@og_bobbee_johnson4",
        "@57poundsoflegosbxnsjwg2iw7xydb",
        "@e.wisdom03",
        "@kingw_24",
        "@arda_akinci_2010",
        "@logancoffey26",
        "@jacob_stark27",
        "@thrown_point",
        "@constance4u",
        "@gabriel_j_d_watts",
        "@oreoafton",
        "@a.vysniauskas",
        "@countryball_norgekommunistisk",
        "@professional_speddie",
        "@viqnnily",
        "@benguzman2408",
        "@shonnabell_",
        "@black_vale03",
        "@demirseyben",
        "@tinyricks_backup",
        "@maxvdvegte",
        "@whothexkruu",
        "@born2_be_yours",
        "@roroyorkk",
        "@rushilyeole",
        "@dinners_by_e",
        "@jqymz16",
        "@dexterackley",
        "@yrn_.ced",
        "@retr0.0fficial",
        "@deucers90",
        "@dtx_j0hn.t",
        "@isaaclb_17",
        "@grantdurner",
        "@al_rashidiya.2024",
        "@you_lamefrbro",
        "@acdog_lover1",
        "@p0oki3bear",
        "@alecho1019",
        "@cody__kl",
        "@zavier_martin29",
        "@featherofdeath_",
        "@yeetgods_son",
        "@rainselias",
        "@badass_jogo_edit_everyday",
        "@felix.a.2711",
        "@m.kaanw",
        "@equaldimee",
        "@dayceehi",
        "@kameziiii",
        "@marco_parangaricutirimicuaro",
        "@george_grandpa",
        "@tomm.yboy72",
        "@amylvjy",
        "@moldykandi",
        "@creed240437",
        "@david.on.two",
        "@_.toasttt._",
        "@geoffreytadda_",
        "@kbrennan05",
        "@cirotarthur",
        "@jaydn6363636362718",
        "@anime.forever31",
        "@vexlesis",
        "@bowser_has_fun",
        "@knife36015",
        "@poptart9073",
        "@tinko_kanu",
        "@kadenmillee",
        "@aaronjacobsaucier",
        "@chace_fuller",
        "@its.nikkinikki",
        "@deacon_ebrown",
        "@mrco55_",
        "@lily_pedersen1",
        "@rosie.isbetter",
        "@wtwndre",
        "@chompyshayo92",
        "@602.gastelum",
        "@whohated_me",
        "@motionhavinnshod",
        "@anonymous_monkeynigga",
        "@chingchongching57",
        "@makifrfr",
        "@irlreferences",
        "@edward_day.2020_was_shit",
        "@lynn_renee6889",
        "@andreinotonyt",
        "@jack_ml_",
        "@dapper_kitten69",
        "@blaze_brunson",
        "@creminiti",
        "@freaky_pharaoh",
        "@exactly_everything",
        "@wyattsherwood71",
        "@jamie_cahill04",
        "@jordanj85660",
        "@curtis_omar_undisputed",
        "@zxke76",
        "@plague_venomx",
        "@thomaspendleton06.15",
        "@baby_back_bitch3",
        "@jamaljohnson1489",
        "@d3yluvhaa",
        "@tofik_651",
        "@i_ate_the_cookies0",
        "@alice_setyon",
        "@gangnoah23",
        "@ryan.lp2011",
        "@lil_dhariel",
        "@smurf_cat123321",
        "@mearsiecat",
        "@the_pandalorian_73",
        "@genuinely_couldnt_fuckin_care",
        "@twizzy.rich24",
        "@marcumr271",
        "@melaniesillay",
        "@sebastianmo73",
        "@jamesonsalsy",
        "@save.angvl",
        "@baraaahmed09",
        "@drbrausefrosch_08",
        "@ghuba.man",
        "@vichopoke",
        "@kianomalley365",
        "@nova_lights00",
        "@gooap_gamrtop3",
        "@biglemons6",
        "@crunchi_da_quattro",
        "@macysdigital.diary",
        "@jakjas221",
        "@_grim49",
        "@jj.tilley_",
        "@huugooooo2.0",
        "@funnyflowerduckduck",
        "@brandon_keogh1",
        "@gilsen38",
        "@jake_the_kat1",
        "@abdallahzzzzam",
        "@jayleningles",
        "@tracyoyster",
        "@ctx.ticcy",
        "@jackriznyk07",
        "@tiny_boi_romeo",
        "@bobertzzz",
        "@fadedbeaner",
        "@joey.oki",
        "@paulino_vitorhugo",
        "@thegoat.347",
        "@mansoor_ali.alt12",
        "@_caden.clark_",
        "@beansfor2days",
        "@guden_tag_mayo",
        "@hryzmman21",
        "@nimrodian2",
        "@forever_is_empty",
        "@nonevoid0s_player",
        "@t1redeve222",
        "@scene.cat",
        "@bewaffeled",
        "@amazing_kidder",
        "@luckeboy_24",
        "@gamernikoplayz",
        "@1tallrundown301",
        "@reeses_cups_cody",
        "@daneservino",
        "@_alex_graff",
        "@daveislarge",
        "@murdayoli",
        "@bigbrocth",
        "@aaron_urbanawiz",
        "@shy_ty_84",
        "@ball_inspector42",
        "@th3_sm1l1n6_shad0w",
        "@letitsshine69",
        "@logan8_8",
        "@jack.short63",
        "@who_lovejordan",
        "@agnes._.huii",
        "@rhettreynolds_150",
        "@lolden_gaptop",
        "@the__coruscant_guard",
        "@cht_aelo",
        "@t3mpur4.0",
        "@samuelkaalund",
        "@blackcar2018",
        "@olivertigane",
        "@evisliving",
        "@lucas.ferraro4",
        "@balivion_",
        "@chr0nic_oce",
        "@t3raj0n",
        "@sol.jxso",
        "@travis_houle04",
        "@spyder_ryder_",
        "@jebat.kaliff",
        "@mauret_07",
        "@dzakyrayxhi",
        "@jakeyy88",
        "@na1lzx",
        "@beratmaybe",
        "@petergamer25",
        "@4everdumping",
        "@the.booky.bug",
        "@lam1n.fr",
        "@t_reasly",
        "@gang_nanto_seiken108",
        "@e_sponjas",
        "@riley_emmi1",
        "@mohmedtarek2001",
        "@jackson_fr100",
        "@realitysetjozen",
        "@bhavnagarmemecorporation",
        "@nz4lc",
        "@earseyxkitty",
        "@titan.399",
        "@marc_5281_",
        "@_omarvillesca_",
        "@gustavo_below",
        "@m13.d330",
        "@c_archie10",
        "@mrmeeskes123",
        "@l_nolte09",
        "@skib_sam",
        "@loic_chamb",
        "@ad3lamac611",
        "@galax.yvr",
        "@gusgdmn",
        "@steamlitebeyond",
        "@the_ozair_yahya",
        "@muddugan",
        "@r_crundwell",
        "@probably_not_me117",
        "@r.dowsing22",
        "@nathanprizm",
        "@carsnart256",
        "@lumiobyte",
        "@lucaspicardabraham",
        "@ryder_swims",
        "@cdog1118",
        "@luki.brg",
        "@yourgirla_69420",
        "@ashlr0527",
        "@ushipoop",
        "@xx.xavier_the_saviour_27.xx",
        "@joe_rogan5",
        "@snomster1",
        "@scythian.turk",
        "@alriceee",
        "@pot_miro",
        "@kc_adri15",
        "@ettawilleatu",
        "@jmarsland12",
        "@andre131989tv",
        "@opium.director",
        "@stxrs4l1z",
        "@the1truewillk",
        "@vanbiakcung_bawi",
        "@the.felix13",
        "@zulian_abdi_rz",
        "@laine.rosner",
        "@flimped1",
        "@irathos806",
        "@markweeres",
        "@gael_zaopap",
        "@loliespolies",
        "@gabriel_012928",
        "@deuces.v2",
        "@widetwelvefiftyfive",
        "@pleaseunhackme",
        "@gabrihas.monet",
        "@playboy_rejectinator",
        "@_idk._.a",
        "@jimthecrim",
        "@ahneekate",
        "@oscar_c_holmes",
        "@marchacontaoficial",
        "@jeelrojello",
        "@davidguzman1928",
        "@some.discordmod",
        "@gustav.kragh",
        "@mr.heracross214",
        "@zhoubickovaneyyy",
        "@tuplioguosey",
        "@nondas_boi",
        "@jaxwhit29",
        "@death._spells",
        "@imbatman0072",
        "@tomomb9",
        "@mason25723",
        "@verde.nathaniel",
        "@miniatureaccompanist",
        "@finnbaker__",
        "@shockley_boom",
        "@anukia.clips",
        "@silly_lamptey",
        "@lilybotwright.x",
        "@siggney1",
        "@eamon_927",
        "@brickmuncherrrr",
        "@aki_portugalac",
        "@tuckerchippy",
        "@mateja7_7",
        "@grimmxpengo",
        "@olivechurro",
        "@legendz5150",
        "@rozem4ry",
        "@slaymepleaseuwu",
        "@kikuriql",
        "@zegory15",
        "@heliotnazariojr",
        "@ial.dc",
        "@detacovenn",
        "@mike_the_kitsune_lover",
        "@tsampikos.kouts",
        "@clement75m",
        "@thomas.mcgraw.9",
        "@ezaye.007",
        "@livqae",
        "@anonimo_bernabeo",
        "@tropical_b0nes",
        "@sy.henry.mak",
        "@liminalchloe",
        "@cor3_ig",
        "@mleighphillips1",
        "@leshwire_",
        "@jakeywad3y",
        "@obviouslybad06",
        "@itzlers",
        "@mohamaad_al_nezar",
        "@soleil_123_",
        "@joshuakewber",
        "@supreme_lordchaos",
        "@govaers03",
        "@narisolit",
        "@ahnaf_the_jimu",
        "@lautaro.pdf",
        "@mahdi_293967",
        "@hidayet.a.atik",
        "@mr.cloud2022",
        "@_rayyy.oo",
        "@spacelucidity",
        "@iefidan03",
        "@ayaan_zaidi._",
        "@wyattogdentr928",
        "@biagi.simo",
        "@_gamb0w_",
        "@tr4iny",
        "@m4rc1n3q",
        "@peter_griffin1235",
        "@felix_taco",
        "@jakdol39",
        "@emanismee",
        "@gabriel_hardjono",
        "@cloverteigan",
        "@gabrielstjules",
        "@domas.pal",
        "@averylhurst2022",
        "@mrpurpletoes",
        "@bartekpro2",
        "@jasms_unu",
        "@timhgb46",
        "@charlieharvey_07",
        "@shore7550",
        "@bellabellabella41",
        "@jett__fr",
        "@erez193",
        "@lagosto43",
        "@90bandzzzz",
        "@dumb_bacha",
        "@yan_is_existing",
        "@mandar_nigade",
        "@b.b.g.z",
        "@derealforever",
        "@higashya.tvt",
        "@clm_dmt",
        "@n.josiah09",
        "@ardikabyakta",
        "@mellowbellowyellowrellow",
        "@the_night_knight_123",
        "@abbacchio.va",
        "@jan_dumancic",
        "@skoczek_wojtek",
        "@khanazhar2402",
        "@aryaanhadi",
        "@exqunged",
        "@ved_shinde07",
        "@__privat.9",
        "@omgxani",
        "@eziosau",
        "@razzberry_pi",
        "@peepi313",
        "@thisismy5thaccount_",
        "@irvinzzz_",
        "@ajgsrj",
        "@dylancathcartt",
        "@iam_gvn_ooi",
        "@arteaga8434",
        "@renatossnt",
        "@ariessbasementtt",
        "@dave.190e",
        "@aidanchangishere",
        "@bestnewsoutlet",
        "@squiggleglimpsewigglefizzfizz",
        "@tom_abs_",
        "@fu3rr0",
        "@ate.withcheese",
        "@astrosidh",
        "@kerim.t34",
        "@memes_copiad",
        "@dunk9d",
        "@neggawattz",
        "@nochillrick",
        "@ethan_pajot",
        "@jamesk9.99",
        "@unidigestible_memes",
        "@ixnzi.8",
        "@looped._.rifat",
        "@aliya.hak",
        "@surip_cc_178",
        "@comedy_cartel",
        "@freakyblueringedoctopus618",
        "@jasonvincenttt",
        "@zach.osterday",
        "@unfiltered_memess",
        "@mmather1119",
        "@onszik",
        "@minionki_360",
        "@gonik27",
        "@yogi_xpoh",
        "@aiku_7",
        "@alvaro._sanz",
        "@econ_logan",
        "@gabinlopez__",
        "@taxsinglemothers",
        "@they.lovemal9",
        "@guigui_fernando",
        "@skrtdob",
        "@kdp_3",
        "@unzuckable",
        "@tastefulmemer",
        "@nochillsimpson",
        "@zuckubus",
        "@unbustable.nuttt",
        "@reallionaire_grindset",
        "@nochillmorty",
        "@nochillshaggy",
        "@levssiq",
        "@imaginaryroola",
        "@aaathenthen451",
        "@_omazi_",
        "@tuckt_ruck",
        "@megajjnistaken",
        "@greenfneveryday",
        "@amy123upr",
        "@chiwawa.testicals",
        "@alwaysmemes99",
        "@subfitss",
        "@_wtmeme",
        "@meme.fent.central",
        "@keiths_paralysis_demon",
        "@yoinkyeetandyoted",
        "@jsjsjwk2483",
        "@wtfke_d",
        "@omari_star_boy",
        "@hugothetotallyrealhugo",
        "@dankvader31",
        "@baldniqqa",
        "@hotaki.ahm",
        "@memecatministry",
        "@izackkx",
        "@mayorbefufflefrumpter",
        "@8fmlfr00",
        "@amin.ah768",
        "@your_memes_only",
        "@memeable.memes",
        "@isabellamson19"};

    public bool IsSimActive { get; private set; }

    private static Sim9GameManager instance;

    private bool isSimEnded = false;

    private Color winnerColor;

    [SerializeField]
    private GameObject topBound;

    [SerializeField]
    private GameObject leaderboardIconPrefab;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private CameraController mainCameraController;

    public static Sim9GameManager Instance
    {
        get
        {
            if (instance is null)
            {
                throw new NullReferenceException("Instance of Sim9GameManager is null");
            }

            return instance;
        }
    }

    private void Awake()
    {
        IsSimActive = false;
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupScene();

        IsSimActive = true;

        Invoke(nameof(StartRace), 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsSimActive)
        {
            return;
        }

        if (isSimEnded)
        {
            var marblesRemaining = FindObjectsOfType<Marble>().Length;
            if (marblesRemaining > 0)
            {
                return;
            }

            IsSimActive = false;
            DisplayEndGamePanel();
        }
    }

    public void EndGame(Color color)
    {
        isSimEnded = true;
        winnerColor = color;
    }

    private void DisplayEndGamePanel()
    {
        endGamePanel.SetActive(true);

        var leaderboardIcon = Instantiate(leaderboardIconPrefab, Vector3.zero, Quaternion.identity);
        leaderboardIcon.transform.SetParent(endGamePanel.transform);
        leaderboardIcon.transform.localScale = new Vector3(WinnerScaleFactor, WinnerScaleFactor);

        var iconRenderer = leaderboardIcon.GetComponent<Renderer>();
        iconRenderer.material.color = winnerColor;
    }

    private void SetupScene()
    {
        var usernameIndex = 0;

        for (var y = SpawnYStart; y < 100f; y += SpawnYOffset)
        {
            for (var x = -SpawnXBound; x <= SpawnXBound + 0.2f; x += SpawnXOffset)
            {
                if (usernameIndex >= usernames.Count)
                {
                    break;
                }

                var spawnPos = new Vector3(x, y, 1f);
                var color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
                Sim9SpawnManager.Instance.SpawnMarble(spawnPos, color, usernames[usernameIndex]);
                usernameIndex++;
            }
        }
    }

    private void StartRace()
    {
        MoveMainCamera(9);
        Destroy(topBound);
    }

    public void MoveMainCamera(float moveAmount = 3.5f)
    {
        mainCameraController.MoveCameraDown(moveAmount);
    }
}
