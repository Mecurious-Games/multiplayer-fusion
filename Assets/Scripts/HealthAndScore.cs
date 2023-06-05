using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class HealthAndScore : NetworkBehaviour
{
    [SerializeField] NumberField HealthDisplay;
    [SerializeField] NumberField ScoreDisplay;
    public bool hasShield = false;

    [Networked(OnChanged = nameof(NetworkedHealthChanged))]
    public int NetworkedHealth { get; set; } = 100;

    [Networked(OnChanged = nameof(NetworkedScoreChanged))]
    public int NetworkedScore { get; set; } = 0;

    private static void NetworkedHealthChanged(Changed<HealthAndScore> changed)
    {
        Debug.Log($"Health changed to: {changed.Behaviour.NetworkedHealth}");
        changed.Behaviour.HealthDisplay.SetNumber(changed.Behaviour.NetworkedHealth);
    }

    private static void NetworkedScoreChanged(Changed<HealthAndScore> changed)
    {
        Debug.Log($"Score changed to: {changed.Behaviour.NetworkedScore}");
        changed.Behaviour.ScoreDisplay.SetNumber(changed.Behaviour.NetworkedScore);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        if (hasShield) return; // Player has a shield, ignore the damage

        NetworkedHealth -= damage;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void AddScoreRpc(int score)
    {
        NetworkedScore += score;
    }

    public void CollectShield()
    {
        hasShield = true;
    }
}
