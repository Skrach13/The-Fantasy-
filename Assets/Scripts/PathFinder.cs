using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static List<Vector2> Path(int[,] pathl ,Vector2 start, Vector2 fisnish)
    {
        List<Vector2> path = FindPath(pathl, start, fisnish);
        Debug.Log(path.Count);
        /*
        var map = GetComponent<BattleSystem>().Map;
        foreach (Vector2 i in path)
        {
            map[(int)i.x, (int)i.y].GetComponent<FloorScripts>().setPicture(sprite);
            Debug.Log(i);

        }
        */
        return path;
    }



    public class PathNode
    {

        // Координаты точки на карте.
        public Vector2 Position { get; set; }
        // Длина пути от старта (G).
        public int PathLengthFromStart { get; set; }
        // Точка, из которой пришли в эту точку.
        public PathNode CameFrom { get; set; }
        // Примерное расстояние до цели (H).
        public int HeuristicEstimatePathLength { get; set; }
        // Ожидаемое полное расстояние до цели (F).
        public int EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }

    }


    public static List<Vector2> FindPath(int[,] field, Vector2 start, Vector2 goal)
    {
        // Шаг 1.Создается 2 списка вершин — ожидающие рассмотрения и уже рассмотренные.
        //       В ожидающие добавляется точка старта, список рассмотренных пока пуст. 
        var closedSet = new Collection<PathNode>();
        var openSet = new Collection<PathNode>();

        // Шаг 2.создание стартовой точки/ определение стартовой точки
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
            // Шаг 3.
            var currentNode = openSet.OrderBy(node =>
              node.EstimateFullPathLength).First();
            // Шаг 4.
            if (currentNode.Position == goal)
                return GetPathForNode(currentNode);
            // Шаг 5.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            // Шаг 6.
            foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
            {
                // Шаг 7.
                if (closedSet.Count(node => node.Position == neighbourNode.Position) > 0)
                    continue;
                var openNode = openSet.FirstOrDefault(node =>
                  node.Position == neighbourNode.Position);
                // Шаг 8.
                if (openNode == null)
                    openSet.Add(neighbourNode);
                else
                  if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                {
                    // Шаг 9.
                    openNode.CameFrom = currentNode;
                    openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                }
            }
        }
        // Шаг 10.
        return null;

    }
    private static int GetDistanceBetweenNeighbours()
    {
        return 1;
    }
    private static int GetHeuristicPathLength(Vector2 from, Vector2 to)
    {
        return (int)(Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y));
    }

    private static Collection<PathNode> GetNeighbours(PathNode pathNode,
         Vector2 goal, int[,] field)
    {
        var result = new Collection<PathNode>();

        // Соседними точками являются соседние по стороне клетки.
        Vector2[] neighbourPoints = new Vector2[6];
        if (pathNode.Position.y % 2 == 0)
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

        foreach (var point in neighbourPoints)
        {
            // Проверяем, что не вышли за границы карты.
            if (point.x < 0 || point.x >= field.GetLength(0))
                continue;
            if (point.y < 0 || point.y >= field.GetLength(1))
                continue;
            // Проверяем, что по клетке можно ходить.
            if ((field[(int)point.x, (int)point.y] != 0) && (field[(int)point.x, (int)point.y] != 1))
                continue;
            // Заполняем данные для точки маршрута.
            var neighbourNode = new PathNode()
            {
                Position = point,
                CameFrom = pathNode,
                PathLengthFromStart = pathNode.PathLengthFromStart +
                GetDistanceBetweenNeighbours(),
                HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
            };
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


}
