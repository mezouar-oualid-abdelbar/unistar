using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterType
    {
        Nail,
        Frost
    }

    public CharacterType selectedCharacter;

    // Drag your prefabs here in Inspector
    public GameObject nailPrefab;
    public GameObject frostyPrefab;

    void Start()
    {
        SpawnCharacter();
    }

    void SpawnCharacter()
    {
        GameObject characterToSpawn = null;

        switch (selectedCharacter)
        {
            case CharacterType.Nail:
                characterToSpawn = nailPrefab;
                break;

            case CharacterType.Frost:
                characterToSpawn = frostyPrefab;
                break;
        }

        if (characterToSpawn != null)
        {
            Instantiate(characterToSpawn, transform.position, Quaternion.identity);
        }
    }
}