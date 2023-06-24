using UnityEngine;

namespace Gameplay.Movement
{
    public class MovementAxis
    {
        public float CurrentCoordinate { get; private set; }
        public float DisplacementStep { get; private set; }
        public int AllowedDisplacementsCountFromCenter { get; private set; }
        public float CenterCoordinate { get; set; }

        private int _currentDisplacementsCountFromCenter;
        
        public MovementAxis(float centerCoordinate, float displacementStep, int count, float currentCoordinate)
        {
            CenterCoordinate = centerCoordinate;
            DisplacementStep = displacementStep;
            AllowedDisplacementsCountFromCenter = count;
            CurrentCoordinate = currentCoordinate;
            CalculateCurrentDisplacementsCountFromCenter();
        }

        public float DisplaceRight()
        {
            if (CheckRightDisplacement())
                return CurrentCoordinate;

            _currentDisplacementsCountFromCenter++;
            CurrentCoordinate += DisplacementStep;

            return CurrentCoordinate;
        }

        public float DisplaceLeft()
        {
            if (CheckLeftDisplacement())
                return CurrentCoordinate;

            _currentDisplacementsCountFromCenter--;
            CurrentCoordinate -= DisplacementStep;

            return CurrentCoordinate;
        }

        public bool CheckRightDisplacement() =>
            _currentDisplacementsCountFromCenter == AllowedDisplacementsCountFromCenter;

        public bool CheckLeftDisplacement() =>
            _currentDisplacementsCountFromCenter == -AllowedDisplacementsCountFromCenter;

        public float DisplaceToFree()
        {
            if (CheckRightDisplacement())
            {
                return DisplaceLeft();
            }
            else if (CheckLeftDisplacement())
            {
                return DisplaceRight();
            }
            else
            {
                return DisplaceRight();
            }
        }

        private void CalculateCurrentDisplacementsCountFromCenter()
        {
            _currentDisplacementsCountFromCenter =
                Mathf.RoundToInt((CurrentCoordinate - CenterCoordinate) / DisplacementStep);
        }

        public static MovementAxis GetDefaultAxis() => new MovementAxis(0, 3.35f, 1, 0);

        public static float[] GetArrayRepresentation(MovementAxis movementAxis)
        {
            int count = (movementAxis.AllowedDisplacementsCountFromCenter * 2) + 1;
            float[] result = new float[count];

            float start = -(movementAxis.AllowedDisplacementsCountFromCenter * movementAxis.DisplacementStep);

            for (int i = 0; i < count; i++)
            {
                result[i] = start;
                start += movementAxis.DisplacementStep;
            }

            return result;
        }

        public static float[] GetArrayRepresentationOfDefault() =>
            GetArrayRepresentation(MovementAxis.GetDefaultAxis());
    }
}