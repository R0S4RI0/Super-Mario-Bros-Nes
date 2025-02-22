using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{
    [SerializeField]
    private int coins;
    public GameObject coin;  // Prefab de la moneda
    public GameObject bloqueInvencible;  // Prefab de bloque invencible
    public GameObject lifePrefab;  // Prefab de la vida
    public GameObject mushroomPrefab;  // Prefab de la seta que hace grande a Mario

    public void Hit()
    {
        base.Hit();

        // Crear la moneda en el bloque
        Instantiate(coin, new Vector3(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);

        this.coins--;

        // Probabilidad de que aparezca una vida o una seta (puedes ajustarlo a tu gusto)
        if (Random.Range(0f, 1f) < 0.5f)  // 50% de probabilidad de obtener una vida
        {
            Instantiate(lifePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
        }
        else  // 50% de probabilidad de obtener una seta
        {
            Instantiate(mushroomPrefab, new Vector3(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
        }

        // Si ya no quedan monedas, hacer que el bloque sea invencible
        if (coins <= 0)
        {
            Instantiate(bloqueInvencible, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);  // Destruir el bloque
        }
    }
}
