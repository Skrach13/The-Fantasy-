using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    /*
     честно спиженный и адаптированый алгоритм (https://lsreg.ru/realizaciya-algoritma-poiska-a-na-c/)
     
    1)Создается 2 списка вершин — ожидающие рассмотрения и уже рассмотренные.В ожидающие добавляется точка старта, список рассмотренных пока пуст.
    2)Для каждой точки рассчитывается F = G + H.G — расстояние от старта до точки, H — примерное расстояние от точки до цели. О расчете этой величины я расскажу позднее.Так же каждая точка хранит ссылку на точку, из которой в нее пришли.
    3)Из списка точек на рассмотрение выбирается точка с наименьшим F. Обозначим ее X.
    4)Если X — цель, то мы нашли маршрут.
    5)Переносим X из списка ожидающих рассмотрения в список уже рассмотренных.
    6)Для каждой из точек, соседних для X (обозначим эту соседнюю точку Y), делаем следующее:
    7)Если Y уже находится в рассмотренных — пропускаем ее.
    8)Если Y еще нет в списке на ожидание — добавляем ее туда, запомнив ссылку на X и рассчитав Y.G (это X.G + расстояние от X до Y) и Y.H.
    9)Если же Y в списке на рассмотрение — проверяем, если X.G + расстояние от X до Y<Y.G, значит мы пришли в точку Y более коротким путем, заменяем Y.G на X.G + расстояние от X до Y, а точку, из которой пришли в Y на X.
    10)Если список точек на рассмотрение пуст, а до цели мы так и не дошли — значит маршрут не существует.
    */





    /// <summary>
    /// Возвращает список вершин графа ввиде Vector2 тоесть путь из точки a(start) в точку b(finish)(по сути прослойка между основным алгоритмом)  
    /// </summary>
    /// <param name="massiveField"> массив ячеек поля (само поле)</param>
    /// <param name="start">стартовай вершина графа </param>
    /// <param name="fisnish">конечная вершина графа</param>
    /// <returns></returns>
    public static List<Vector2> Path(CellFloorScripts[,] massiveField, Vector2 start, Vector2 fisnish)
    {

        List<Vector2> path = FindPath(massiveGraff(massiveField), start, fisnish);
        Debug.Log($"Длина пути равна : {path.Count}");
        foreach(Vector2 v in path)
        {
            Debug.Log("точки пути :" + v);
        }
          
        return path;
    }
    /// <summary>
    /// покраска ячеек по построеному пути ( вызываеться где нужно)
    /// </summary>
    /// <param name="path">массив содержащий путь по вершинам графов </param>
    /// <param name="massiveField">массив ячеек(поле) </param>
    /// <param name="color">цвет покраски ячеек</param>
    public static void paintPath(List<Vector2> path, CellFloorScripts[,] massiveField, Color color)
    {
        foreach (Vector2 coordinat in path)
        {
            massiveField[(int)coordinat.x, (int)coordinat.y].spriteRenderer.color = color;
        }
    }

    /// <summary>
    /// прослойка для поиска пути указывающая какие ечейки на поле закрыты
    /// </summary>
    /// <param name="massiveFields">массив ячеек (поле)</param>
    /// <returns></returns>
    private static int[,] massiveGraff(CellFloorScripts[,] massiveFields)
    {
        int[,] massiceGraff = new int[massiveFields.GetUpperBound(0) + 1, massiveFields.GetUpperBound(1) + 1];//создание массива со сторанами = поля ячеек
        for (int x = 0; x < massiveFields.GetUpperBound(0) + 1; x++)
        {
            for (int y = 0; y < massiveFields.GetUpperBound(1) + 1; y++)
            {
                if (massiveFields[x, y].closeCell)
                {
                    massiceGraff[x, y] = 9;// закрытая ячейка
                }
                else
                {
                    massiceGraff[x, y] = 0;// открытая для движения
                }
            }
        }

        return massiceGraff;
    }

    /// <summary>
    /// Создание  вершины графа 
    /// </summary>
    public class PathNode
    {

        // Координаты точки на карте.
        public Vector2 Position { get; set; }
        // Длина пути от старта (G).
        public float PathLengthFromStart { get; set; }
        // Точка, из которой пришли в эту точку.
        public PathNode CameFrom { get; set; }
        // Примерное расстояние до цели (H).
        public float HeuristicEstimatePathLength { get; set; }
        // Ожидаемое полное расстояние до цели (F).
        public float EstimateFullPathLength
        {
            get
            {
                return this.PathLengthFromStart + this.HeuristicEstimatePathLength;
            }
        }

    }


    /// <summary>
    /// Сам алгоритм поиска пути
    /// </summary>
    /// <param name="field"></param>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
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
    private static int GetDistanceBetweenNeighbours(int countPoint)
    {
       
        return 1;
       
    }
    /// <summary>
    /// Получить эвристическую длину пути (примерной оценка ожидаемого пути)
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    private static int GetHeuristicPathLength(Vector2 from, Vector2 to)
    {
        //Math.abs если очень просто то убирает знак минус у значений ( возвращает абсолютное значение)
        return (int)(Math.Abs(from.x - to.x) + Math.Abs(from.y - to.y));
    }

    /// <summary>
    /// создание списка(колекции/массива) соседей для обозреваемой точки
    /// </summary>
    /// <param name="pathNode"></param>
    /// <param name="goal"></param>
    /// <param name="field"></param>
    /// <returns></returns>
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
        
            int countPoint=0;
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
                    GetDistanceBetweenNeighbours(countPoint),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
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


    
}
