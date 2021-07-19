using System.Collections.Generic;

public class CharacterManager : Singleton<CharacterManager>
{
    private Character player;
    public Character Player { get { return player == null ? player = FindObjectOfType<Character>() : player; } }

    private List<AI> aiCharacters;
    public List<AI> AICharacters { get { return aiCharacters == null ? aiCharacters = new List<AI>() : aiCharacters; } }

    public void AddCharacter(AI ai)
    {
        if (!AICharacters.Contains(ai))
            AICharacters.Add(ai);
    }

    public void RemoveCharacter(AI ai)
    {
        if (AICharacters.Contains(ai))
            AICharacters.Remove(ai);
    }
}
