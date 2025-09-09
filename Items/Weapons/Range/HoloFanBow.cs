using HololiveMod.Items.Placeables.Ores;
using HololiveMod.Tiles.Workbenches;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace HololiveMod.Items.Weapons.Range
{
    public class HoloFanBow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 22;
            Item.height = 36;
            Item.useTime = 14;
            Item.useAnimation = 14;

            Item.useStyle = ItemUseStyleID.Shoot;

            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;

            Item.knockBack = 5;
            Item.noMelee = true;

            Item.shoot = ProjectileID.FireArrow;
            Item.shootSpeed = 5;

            Item.useAmmo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<HoloOre>(7);
            recipe.AddTile<HoloOfficeTile>();
            recipe.Register();
        }
    }
}
