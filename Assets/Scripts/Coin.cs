using UnityEngine;

public class Coin : MonoBehaviour
{

    public bool coinMag = false;
    public float Timer = 0.0f;


    void Update()
    {
        transform.Rotate(130 * Time.deltaTime, 0, 0);

        if(coinMag == true)
        {
            Timer += 1 * Time.deltaTime;

            if(Timer >= 10)
            {
                coinMag = false;
                Timer = 0.0f;
                Debug.Log(coinMag);
            }
        }

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] CurrentCoins = GameObject.FindGameObjectsWithTag("Coin");
        GameObject Magnet = GameObject.FindGameObjectWithTag("Magnet");



        foreach(GameObject Coin in CurrentCoins)
        {
            if(coinMag == true)
            {
                if (Vector3.Distance(Coin.transform.position, Player.transform.position) < 5) { 
                    Coin.transform.Translate((Player.transform.position - Coin.transform.position).normalized * 4 * Time.deltaTime, Space.World);
                
                }
            }
        }


        if (Magnet != null)
        {
            if(Vector3.Distance(Magnet.transform.position, Player.transform.position ) < 0.5)
            {
                if (coinMag == false)
                {
                    coinMag = true;
                    Debug.Log(coinMag);

                }

                Destroy(GameObject.FindGameObjectWithTag("Magnet"));

                
            }
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
            PlayerManager.numberOfCoins += 1;
            Destroy(gameObject);

            

        }

        
    }
}
