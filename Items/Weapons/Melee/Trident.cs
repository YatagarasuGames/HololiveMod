using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using HololiveMod.Projectiles.Melee;

namespace HololiveMod.Items.Weapons.Melee
{
    // This is a basic item template.
    // Please see tModLoader's ExampleMod for every other example:
    // https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
    public class Trident : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.width = 44;
            Item.height = 88;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 6;
            Item.value = Item.buyPrice(silver: 100);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;

            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.shootSpeed = 3.7f;
            Item.shoot = ModContent.ProjectileType<TridentProjectile>();
            //Item.shoot = ModContent.ProjectileType<TridentFlyingProjectile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}

