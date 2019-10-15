﻿using System;

namespace Jopalesha.Common.Client.Http.Components
{
    internal struct ChallengeSolution : IEquatable<ChallengeSolution>
    {
        public ChallengeSolution(string clearancePage, string verificationCode, string pass, int answer)
        {
            ClearancePage = clearancePage;
            VerificationCode = verificationCode;
            Pass = pass;
            Answer = answer;
        }

        public string ClearancePage { get; }

        public string VerificationCode { get; }

        public string Pass { get; }

        public int Answer { get; }

        public string ClearanceQuery =>
            $"{ ClearancePage}?jschl_vc={ VerificationCode}&pass={ Pass}&jschl_answer={ Answer}";

        public static bool operator ==(ChallengeSolution solutionA, ChallengeSolution solutionB)
        {
            return solutionA.Equals(solutionB);
        }

        public static bool operator !=(ChallengeSolution solutionA, ChallengeSolution solutionB)
        {
            return !(solutionA == solutionB);
        }

        public override bool Equals(object obj)
        {
            var nullable = obj as ChallengeSolution?;
            return nullable.HasValue && Equals(nullable.Value);
        }

        public override int GetHashCode()
        {
            return ClearanceQuery.GetHashCode();
        }

        public bool Equals(ChallengeSolution other)
        {
            return other.ClearanceQuery == ClearanceQuery;
        }
    }
}