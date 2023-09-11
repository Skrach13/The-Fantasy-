using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PathFinderInBattle : MonoBehaviour
{
    /*
     ������ ��������� � ������������� �������� (https://lsreg.ru/realizaciya-algoritma-poiska-a-na-c/)
     
    1)��������� 2 ������ ������ � ��������� ������������ � ��� �������������.� ��������� ����������� ����� ������, ������ ������������� ���� ����.
    2)��� ������ ����� �������������� F = G + H.G � ���������� �� ������ �� �����, H � ��������� ���������� �� ����� �� ����. � ������� ���� �������� � �������� �������.��� �� ������ ����� ������ ������ �� �����, �� ������� � ��� ������.
    3)�� ������ ����� �� ������������ ���������� ����� � ���������� F. ��������� �� X.
    4)���� X � ����, �� �� ����� �������.
    5)��������� X �� ������ ��������� ������������ � ������ ��� �������������.
    6)��� ������ �� �����, �������� ��� X (��������� ��� �������� ����� Y), ������ ���������:
    7)���� Y ��� ��������� � ������������� � ���������� ��.
    8)���� Y ��� ��� � ������ �� �������� � ��������� �� ����, �������� ������ �� X � ��������� Y.G (��� X.G + ���������� �� X �� Y) � Y.H.
    9)���� �� Y � ������ �� ������������ � ���������, ���� X.G + ���������� �� X �� Y<Y.G, ������ �� ������ � ����� Y ����� �������� �����, �������� Y.G �� X.G + ���������� �� X �� Y, � �����, �� ������� ������ � Y �� X.
    10)���� ������ ����� �� ������������ ����, � �� ���� �� ��� � �� ����� � ������ ������� �� ����������.
    */

    /// <summary>
    /// ���������� ������ ������ ����� ����� Vector2 ������ ���� �� ����� a(start) � ����� b(finish)(�� ���� ��������� ����� �������� ����������)  
    /// </summary>
    /// <param name="massiveField"> ������ ����� ���� (���� ����)</param>
    /// <param name="start">��������� ������� ����� </param>
    /// <param name="fisnish">�������� ������� �����</param>
    /// <returns></returns>
    public static List<Vector2> Path(CellInBattle[,] massiveField, Vector2 start, Vector2 fisnish)
    {
        List<Vector2> path = FindPath(massiveField, start, fisnish);
        return path;
    }
    /// <summary>
    /// �������� ����� �� ����������� ���� ( ����������� ��� �����)
    /// </summary>
    /// <param name="path">������ ���������� ���� �� �������� ������ </param>
    /// <param name="massiveField">������ �����(����) </param>
    /// <param name="color">���� �������� �����</param>
    public static void PaintPath(List<Vector2> path, CellInBattle[,] massiveField, Color color)
    {
        foreach (Vector2 coordinat in path)
        {
            massiveField[(int)coordinat.x, (int)coordinat.y].paintCellBattle(color);
        }
    }

    /// <summary>
    /// ��������� ��� ������ ���� ����������� ����� ������ �� ���� �������
    /// </summary>
    /// <param name="massiveFields">������ ����� (����)</param>
    /// <returns></returns>
    private static int[,] massiveGraff(CellInBattle[,] massiveFields)
    {
        int[,] massiceGraff = new int[massiveFields.GetUpperBound(0) + 1, massiveFields.GetUpperBound(1) + 1];//�������� ������� �� ��������� = ���� �����
        for (int x = 0; x < massiveFields.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < massiveFields.GetUpperBound(1) + 1; y++)
            {
                if (massiveFields[x, y].CloseCell)
                {
                    massiceGraff[x, y] = 9;// �������� ������
                }
                else
                {
                    massiceGraff[x, y] = 0;// �������� ��� ��������
                }
            }
        }

        return massiceGraff;
    }

    /// <summary>
    /// ��������  ������� ����� 
    /// </summary>
    public class PathNode
    {

        // ���������� ����� �� �����.
        public Vector2 Position { get; set; }
        // ����� ���� �� ������ (G).
        public float PathLengthFromStart { get; set; }
        // �����, �� ������� ������ � ��� �����.
        public PathNode CameFrom { get; set; }
        // ��������� ���������� �� ���� (H).
        public float HeuristicEstimatePathLength { get; set; }
        // ��������� ������ ���������� �� ���� (F).
        public float EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }

    }


    /// <summary>
    /// ��� �������� ������ ����
    /// </summary>
    /// <param name="field"></param>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    private static List<Vector2> FindPath(CellInBattle[,] field, Vector2 start, Vector2 goal)
    {
        // ��� 1.��������� 2 ������ ������ � ��������� ������������ � ��� �������������.
        //       � ��������� ����������� ����� ������, ������ ������������� ���� ����. 
        var closedSet = new Collection<PathNode>();
        var openSet = new Collection<PathNode>();

        // ��� 2.�������� ��������� �����/ ����������� ��������� �����
        PathNode startNode = new PathNode()
        {
            Position = start,
            CameFrom = null,
            PathLengthFromStart = 0,
            HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
        };
        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            // ��� 3.����� ������ ���� �� ���������������� ������ (�� ���������� ������� ����) 
            var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();
            // ��� 4.��������� ������� ���� �� ������ �� �� � ����, � ���� ������ ���������� ���� � ������� �� �����
            if (currentNode.Position == goal) return GetPathForNode(currentNode);
            // ��� 5.������ ������������� ���� �� ������ �� �������������
            openSet.Remove(currentNode);
            //��������� ������������ ���� � ������ �������������
            closedSet.Add(currentNode);
            // ��� 6.
            foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
            {
                // ��� 7.��������� ��� �� �������� ���� ����������, ���� �� ������� � ���������� ���� ( �������� ����� ����� ���������� �� �������)
                if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;
                // ��������� ���� �� � �� ������������� ����� ������� ����� ( �������� ����� ����� ���������� �� �������)
                var openNode = openSet.FirstOrDefault(node => node.Position == neighbourNode.Position);
                // ��� 8.//���� � �� ������������ ����� ��� �������� ������ �� ��������� � ������ �� ������������
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else //���� �� Y � ������ �� ������������ � ���������, ���� X.G + ���������� �� X �� Y<Y.G, ������ �� ������ � ����� Y ����� �������� �����, �������� Y.G �� X.G + ���������� �� X �� Y, � �����, �� ������� ������ � Y �� X.
                  if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    // ��� 9.
                    openNode.CameFrom = currentNode;
                    openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }
        // ��� 10.
        return null;

    }

    /// <summary>
    /// �������� ������(��������/�������) ������� ��� ������������ �����
    /// </summary>
    /// <param name="pathNode"></param>
    /// <param name="goal"></param>
    /// <param name="field"></param>
    /// <returns></returns>
    private static Collection<PathNode> GetNeighbours(PathNode pathNode,
         Vector2 goal, CellInBattle[,] field)
    {
        var result = new Collection<PathNode>();

        // ��������� ������� �������� �������� �� ������� ������.
        Vector2[] neighbourPoints = new Vector2[6];
        if (pathNode.Position.y % 2 != 0)
        {
            neighbourPoints[0] = new Vector2(pathNode.Position.x - 1, pathNode.Position.y);//left
            neighbourPoints[1] = new Vector2(pathNode.Position.x - 1, pathNode.Position.y - 1);//left down
            neighbourPoints[2] = new Vector2(pathNode.Position.x, pathNode.Position.y - 1);//down right
            neighbourPoints[3] = new Vector2(pathNode.Position.x + 1, pathNode.Position.y);//right
            neighbourPoints[4] = new Vector2(pathNode.Position.x, pathNode.Position.y + 1); // up right
            neighbourPoints[5] = new Vector2(pathNode.Position.x - 1, pathNode.Position.y + 1);// up left 

        }
        else
        {
            neighbourPoints[0] = new Vector2(pathNode.Position.x - 1, pathNode.Position.y);//left
            neighbourPoints[1] = new Vector2(pathNode.Position.x, pathNode.Position.y - 1);//down left
            neighbourPoints[2] = new Vector2(pathNode.Position.x + 1, pathNode.Position.y - 1);//down right
            neighbourPoints[3] = new Vector2(pathNode.Position.x + 1, pathNode.Position.y);//right
            neighbourPoints[4] = new Vector2(pathNode.Position.x + 1, pathNode.Position.y + 1);//up right
            neighbourPoints[5] = new Vector2(pathNode.Position.x, pathNode.Position.y + 1);//up left
        }

        int countPoint = 0;
        foreach (var point in neighbourPoints)
        {
            // ���������, ��� �� ����� �� ������� �����.
            if (point.x < 0 || point.x >= field.GetLength(0))
                continue;
            if (point.y < 0 || point.y >= field.GetLength(1))
                continue;
            // ���������, ��� �� ������ ����� ������.
           // Debug.Log($"{field[(int)point.x, (int)point.y].PositionInGraff}");
            if (field[(int)point.x, (int)point.y].CloseCell == true)
                continue;
            // ��������� ������ ��� ����� ��������.
            var neighbourNode = new PathNode()
            {
                Position = point,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart +
                GetDistanceBetweenNeighbours(field[(int)point.x, (int)point.y].RibsWeight, field[(int)pathNode.Position.x, (int)pathNode.Position.y].RibsWeight),
                HeuristicEstimatePathLength = GetHeuristicPathLength(field[(int)point.x, (int)point.y].PositionInGraff, field[(int)goal.x, (int)goal.y].PositionInGraff)
            };

            countPoint++;
            result.Add(neighbourNode);
        }
        return result;
    }

    private static List<Vector2> GetPathForNode(PathNode pathNode)
    {
        var result = new List<Vector2>();
        var currentNode = pathNode;
        while (currentNode != null)
        {
            result.Add(currentNode.Position);
            currentNode = currentNode.CameFrom;
        }
        result.Reverse();
        return result;
    }
    private static float GetDistanceBetweenNeighbours(float weightNode , float weightPreviousNode)
    {
        if(weightNode < weightPreviousNode)
        {
            return weightPreviousNode;
        }
        else
        {
            return weightNode;
        }
    }
    /// <summary>
    /// �������� ������������� ����� ���� (��������� ������ ���������� ����)
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private static int GetHeuristicPathLength(Vector2 from, Vector2 to)
    {
        //Math.abs ���� ����� ������ �� ������� ���� ����� � �������� ( ���������� ���������� ��������)
        return (int)(Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y));
    }



}
