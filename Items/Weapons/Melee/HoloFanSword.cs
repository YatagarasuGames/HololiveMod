using HololiveMod.Items.Placeables.Ores;
using HololiveMod.Tiles.Workbenches;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.Items.Weapons.Melee
{
    public class HoloFanSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 21;
            Item.DamageType = DamageClass.Melee;
            Item.width = 50;
            Item.height = 54;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 6;
            Item.value = Item.sellPrice(silver: 100);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<HoloOre>(12);
            recipe.AddTile<HoloOfficeTile>();
            recipe.Register();
        }
    }
}
