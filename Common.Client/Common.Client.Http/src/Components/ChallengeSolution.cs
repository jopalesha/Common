using System;

namespace Jopalesha.Common.Client.Http.Components;

/// <summary>
/// Solution for cloudflare defense.
/// </summary>
internal readonly struct ChallengeSolution : IEquatable<ChallengeSolution>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChallengeSolution"/> struct.
    /// </summary>
    /// <param name="clearancePage">Page.</param>
    /// <param name="verificationCode">Verification code.</param>
    /// <param name="pass">Pass.</param>
    /// <param name="answer">Answer.</param>
    public ChallengeSolution(
        string clearancePage,
        string verificationCode,
        string pass,
        int answer)
    {
        ClearancePage = clearancePage;
        VerificationCode = verificationCode;
        Pass = pass;
        Answer = answer;
    }

    /// <summary>
    /// Gets clearance page.
    /// </summary>
    public string ClearancePage { get; }

    /// <summary>
    /// Gets verification code.
    /// </summary>
    public string VerificationCode { get; }

    /// <summary>
    /// Gets pass.
    /// </summary>
    public string Pass { get; }

    /// <summary>
    /// Gets answer.
    /// </summary>
    public int Answer { get; }

    /// <summary>
    /// Gets query.
    /// </summary>
    public string ClearanceQuery =>
        $"{ClearancePage}?jschl_vc={VerificationCode}&pass={Pass}&jschl_answer={Answer}";

    public static bool operator ==(ChallengeSolution solutionA, ChallengeSolution solutionB)
    {
        return solutionA.Equals(solutionB);
    }

    public static bool operator !=(ChallengeSolution solutionA, ChallengeSolution solutionB)
    {
        return !(solutionA == solutionB);
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        return obj is ChallengeSolution nullable && Equals(nullable);
    }

    /// <inheritdoc />
    public override int GetHashCode() => ClearanceQuery.GetHashCode();

    /// <inheritdoc />
    public bool Equals(ChallengeSolution other) => other.ClearanceQuery == ClearanceQuery;
}
