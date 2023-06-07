using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class HealthAndScore : NetworkBehaviour
{
    // [SerializeField] NumberField HealthDisplay;
    // [SerializeField] NumberField ScoreDisplay;
    [SerializeField] TMPro.TextMeshProUGUI HealthText;
    [SerializeField] TMPro.TextMeshProUGUI ScoreText;
    public bool hasShield = false;

    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;

    [Networked(OnChanged = nameof(NetworkedScoreChanged))]
    public int NetworkedScore { get; set; } = 0;

    private static void NetworkedHealthChanged(Changed<HealthAndScore> changed)
    {
        Debug.Log($"Health changed to: {changed.Behaviour.NetworkedHealth}");
        // changed.Behaviour.HealthDisplay.SetNumber(changed.Behaviour.NetworkedHealth);
    }

    private static void NetworkedScoreChanged(Changed<HealthAndScore> changed)
    {
        Debug.Log($"Score changed to: {changed.Behaviour.NetworkedScore}");
        // changed.Behaviour.ScoreDisplay.SetNumber(changed.Behaviour.NetworkedScore);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        if (hasShield) return; // Player has a shield, ignore the damage

        NetworkedHealth -= damage;
        if (NetworkedHealth <= 0)
        {
            // end game
            Debug.Log("Game Over");
            SceneManager.LoadScene("GameOverScene");
        }
        HealthText.text = "Health : " + NetworkedHealth.ToString();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void AddScoreRpc(int score)
    {
        NetworkedScore += score;
        ScoreText.text = "Score : " + NetworkedScore.ToString();
    }

    public void CollectShield()
    {
        hasShield = true;
    }

    public override void Spawned()
    {
        HealthText = GameObject.Find("HealthText").GetComponent<TMPro.TextMeshProUGUI>();
        ScoreText = GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>();
        if (HealthText == null || ScoreText == null)
        {
            Debug.LogError("HealthText or ScoreText not found");
        }
        else
        {
            ScoreText.text = "Score : " + NetworkedScore.ToString();
            HealthText.text = "Health : " + NetworkedHealth.ToString();
        }
    }
}
