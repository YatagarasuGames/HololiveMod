using HololiveMod.Items.Placeables.Ores;
using HololiveMod.Projectiles.Magic;
using HololiveMod.Tiles.Workbenches;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace HololiveMod.Items.Weapons.Magic
{
    public class HoloFanBook : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 16;
            Item.DamageType = DamageClass.Magic;
            Item.width = 22;
            Item.height = 36;
            Item.useTime = 14;
            Item.useAnimation = 14;


            Item.useStyle = ItemUseStyleID.Shoot;

            Item.autoReuse = true;
            Item.mana = 6;
            Item.UseSound = SoundID.Item71;
            Item.knockBack = 5;
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<HoloBookProjectile>();
            Item.shootSpeed = 5;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<HoloOre>(7);
            recipe.AddIngredient(ItemID.Book);
            recipe.AddTile<HoloOfficeTile>();
            recipe.Register();
        }
    }
}
