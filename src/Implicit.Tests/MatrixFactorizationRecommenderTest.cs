﻿using System.Linq;
using Xunit;

namespace Implicit.Tests
{
    public abstract class MatrixFactorizationRecommenderTest<TMatrixFactorizationRecommender> : RecommenderTest<TMatrixFactorizationRecommender>
        where TMatrixFactorizationRecommender : IMatrixFactorizationRecommender
    {
        [Fact]
        public void RecommendUserForItem()
        {
            var n = 50;
            var data = this.CreateCheckerBoard(n);
            var recommender = this.CreateRecommender(data);

            foreach (var itemId in Enumerable.Range(0, n).Select(o => o.ToString()))
            {
                var user = recommender.ComputeUserFactors(new[] { itemId });
                var items = recommender.RecommendUser(user).Take(11).Select(o => o.ItemId);

                var parity = items
                    .Select(o => int.Parse(o) % 2)
                    .GroupBy(o => o)
                    .OrderByDescending(o => o.Count())
                    .Select(o => o.Key)
                    .First();

                Assert.Equal(parity, int.Parse(itemId) % 2);
            }
        }

        [Fact]
        public void RecommendUserForItems()
        {
            var n = 50;
            var data = this.CreateCheckerBoard(n);
            var recommender = this.CreateRecommender(data);
            
            foreach (var userId in Enumerable.Range(0, n).Select(o => o.ToString()))
            {
                var user = recommender.ComputeUserFactors(data[userId].Keys);
                var items1 = recommender.RecommendUser(userId).Take(25).Select(o => o.ItemId);
                var items2 = recommender.RecommendUser(user).Take(25).Select(o => o.ItemId);

                Assert.True(items1.SequenceEqual(items2));
            }
        }
    }
}
