using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        Debug.Log("Standard Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
    }
    public void PurchaseMissileTurret()
    {
        Debug.Log("Missile Turret Purchased");
        buildManager.SetTurretToBuild(buildManager.missileTurretPrefab);
    }
}
