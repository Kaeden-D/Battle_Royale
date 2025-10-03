using UnityEngine;
using Photon.Pun;

public enum PickupType
{
    Health,
    Ammo,
    Speed,
    Jump,
    Damage,
    FireRate
}

public class Pickup : MonoBehaviourPun
{

    public PickupType type;
    public float value;

    void OnTriggerEnter(Collider other)
    {

        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
            
        if (other.CompareTag("Player"))
        {

            // get the player
            PlayerController player = GameManager.instance.GetPlayer(other.gameObject);
            if (type == PickupType.Health)
            {
                player.photonView.RPC("Heal", player.photonPlayer, value);
            }
            else if (type == PickupType.Ammo)
            {
                player.photonView.RPC("GiveAmmo", player.photonPlayer, value);
            }
            else if (type == PickupType.Speed)
            {
                player.photonView.RPC("ChangeSpeed", player.photonPlayer, value);
            }
            else if (type == PickupType.Jump)
            {
                player.photonView.RPC("ChangeJump", player.photonPlayer, value);
            }
            else if (type == PickupType.Damage)
            {
                player.photonView.RPC("ChangeDamage", player.photonPlayer, value);
            }
            else if (type == PickupType.FireRate)
            {
                player.photonView.RPC("ChangeFireRate", player.photonPlayer, value);
            }

            // destroy the object
            photonView.RPC("DestroyPickup", RpcTarget.AllBuffered);

        }

    }

    [PunRPC]
    public void DestroyPickup()
    {
        Destroy(gameObject);
    }

}
