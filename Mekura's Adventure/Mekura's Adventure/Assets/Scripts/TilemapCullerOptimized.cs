

// На это забей я нормально это переделаю


















//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Tilemaps;

//[RequireComponent(typeof(Tilemap))]
//public class TilemapColliderOptimizer : MonoBehaviour
//{
//    [Header("Настройки оптимизации")]
//    [SerializeField] private float updateInterval = 0.1f;
//    [SerializeField] private float screenMargin = 2f; // Запас за границами экрана

//    private Tilemap tilemap;
//    private CompositeCollider2D compositeCollider;
//    private List<Collider2D> tileColliders = new List<Collider2D>();
//    private Camera mainCamera;
//    private float timer;

//    // Границы экрана в мировых координатах
//    private Bounds screenBounds;

//    void Start()
//    {
//        tilemap = GetComponent<Tilemap>();
//        compositeCollider = GetComponent<CompositeCollider2D>();
//        mainCamera = Camera.main;

//        // Получаем все коллайдеры тайлов
//        GetAllTileColliders();

//        // Инициализируем границы экрана
//        UpdateScreenBounds();
//    }

//    void GetAllTileColliders()
//    {
//        tileColliders.Clear();

//        // Если используем CompositeCollider2D
//        if (compositeCollider != null)
//        {
//            // CompositeCollider уже объединяет все коллайдеры
//            // Можно работать с ним как с единым объектом
//        }
//        else
//        {
//            // Если используем отдельные коллайдеры на каждом тайле
//            var colliders = GetComponentsInChildren<Collider2D>();
//            tileColliders.AddRange(colliders);
//        }
//    }

//    void Update()
//    {
//        timer += Time.deltaTime;

//        // Обновляем с заданным интервалом (для производительности)
//        if (timer >= updateInterval)
//        {
//            UpdateScreenBounds();
//            OptimizeColliders();
//            timer = 0f;
//        }
//    }

//    void UpdateScreenBounds()
//    {
//        if (mainCamera == null)
//        {
//            mainCamera = Camera.main;
//            if (mainCamera == null) return;
//        }

//        // Получаем границы видимой области камеры
//        float screenAspect = (float)Screen.width / Screen.height;
//        float cameraHeight = mainCamera.orthographicSize * 2f;
//        Bounds bounds = new Bounds(
//            mainCamera.transform.position,
//            new Vector3(cameraHeight * screenAspect + screenMargin,
//                       cameraHeight + screenMargin,
//                       100f)
//        );

//        screenBounds = bounds;
//    }

//    void OptimizeColliders()
//    {
//        if (compositeCollider != null)
//        {
//            // Оптимизация для CompositeCollider2D
//            OptimizeCompositeCollider();
//        }
//        else if (tileColliders.Count > 0)
//        {
//            // Оптимизация для отдельных коллайдеров
//            OptimizeIndividualColliders();
//        }
//    }

//    void OptimizeCompositeCollider()
//    {
//        // Простая оптимизация - включаем/выключаем весь CompositeCollider
//        bool isVisible = screenBounds.Intersects(compositeCollider.bounds);
//        compositeCollider.enabled = isVisible;
//    }

//    void OptimizeIndividualColliders()
//    {
//        int enabledCount = 0;
//        int disabledCount = 0;

//        foreach (var collider in tileColliders)
//        {
//            if (collider == null) continue;

//            // Проверяем, находится ли коллайдер в пределах экрана
//            bool isVisible = screenBounds.Intersects(collider.bounds);

//            // Включаем/выключаем коллайдер
//            collider.enabled = isVisible;

//            // Считаем статистику
//            if (isVisible) enabledCount++;
//            else disabledCount++;
//        }

//        // Логируем статистику (можно убрать в релизе)
//        Debug.Log($"Tilemap Colliders: {enabledCount} активны, {disabledCount} отключены");
//    }

//    // Визуализация границ экрана в редакторе
//    void OnDrawGizmosSelected()
//    {
//        if (!Application.isPlaying) return;

//        Gizmos.color = Color.yellow;
//        Gizmos.DrawWireCube(screenBounds.center, screenBounds.size);
//    }
//}