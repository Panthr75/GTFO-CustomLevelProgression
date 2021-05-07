namespace CustomLevelProgression.Wrappers.WGear
{
    public interface iResourcePackReceiverWrapper
    {
        bool NeedHealth();

        bool NeedDisinfection();

        bool NeedWeaponAmmo();

        bool NeedToolAmmo();

        bool IsLocallyOwned { get; }

        string InteractionName { get; }

        void GiveAmmoRel(float ammoStandardRel, float ammoSpecialRel, float ammoClassRel);

        void GiveHealth(float health);

        void GiveDisinfection(float disinfection);
    }
}