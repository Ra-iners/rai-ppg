using UnityEngine; //You'll probably need this...

namespace Mod
{
    public class Mod
    {
        public static void Main()
        {
            //This method is the entry point
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"), //item to derive from
                    NameOverride = "Rai", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "Literally my fursona :flushed:", //new item description
                    CategoryOverride = ModAPI.FindCategory("Entities"), //new item category
                    ThumbnailOverride = ModAPI.LoadSprite("thumb.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        //load textures for each layer (see Human textures folder in this repository)
                        var skin = ModAPI.LoadTexture("skin.png");
                        var flesh = ModAPI.LoadTexture("flesh.png");
                        var bone = ModAPI.LoadTexture("bones.png");
                        //get person
                        var person = Instance.GetComponent<PersonBehaviour>();

                        // Add ze fuckn tail
                        GameObject TailOBJ = new GameObject("tail");
                        TailOBJ.transform.SetParent(Instance.transform.Find("Body").Find("LowerBody"));
                        TailOBJ.transform.localPosition = new Vector3(-0.7521f, -0f);
                        TailOBJ.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        TailOBJ.transform.localScale = new Vector3(0.25f, 0.25f);
                        SpriteRenderer RenderTail = TailOBJ.AddComponent<SpriteRenderer>();
                        RenderTail.sprite = ModAPI.LoadSprite("tail.png", 1f, true);
                        RenderTail.sortingLayerName = "Top";

                        // Add ze fuckn visor
                        GameObject VisorTM = new GameObject("head");
                        Transform child = Instance.transform.GetChild(5);
                        VisorTM.transform.SetParent(child);
                        VisorTM.transform.localPosition = new Vector3(-0f, -0f);
                        VisorTM.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                        VisorTM.transform.localScale = new Vector3(1f, 1f);
                        SpriteRenderer RenderVisor = VisorTM.AddComponent<SpriteRenderer>();
                        RenderVisor.sprite = ModAPI.LoadSprite("visor.png", 1f, true);
                        RenderVisor.sortingLayerName = "Top";

                        //use the helper function to set each texture
                        //parameters are as follows: 
                        //  skin texture, flesh texture, bone texture, sprite scale
                        //you can pass "null" to fall back to the original texture
                        person.SetBodyTextures(skin, flesh, bone, 1);

                        //change procedural damage colours if they interfere with your texture (rgb 0-255)
                        person.SetBruiseColor(86, 62, 130); //main bruise colour. purple-ish by default
                        person.SetSecondBruiseColor(181, 10, 118); //second bruise colour. red by default
                        person.SetThirdBruiseColor(135, 8, 88); // third bruise colour. light yellow by default
                        person.SetRottenColour(135, 55, 105); // rotten/zombie colour. light yellow/green by default
                        person.SetBloodColour(247, 101, 193); // blood colour. dark red by default. note that this does not change decal nor particle effect colours. it only affects the procedural blood color which may or may not be rendered
                    }
                }
            );
        }
    }
}