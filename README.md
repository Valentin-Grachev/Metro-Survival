# Metro-Survival
Примечание к коду: Методы Update, FixedUpdate, OnEnable и OnDisable во многих классах заменены на соответствующие Run, FixedRun, OnEnabled, OnDisabled - наследуются от MonoCache вместо MonoBehavior. (Вроде как эти методы работают на 30% быстрее, ссылка на видео, откуда они взяты https://www.youtube.com/watch?v=7Dvir9Bf8X4&t=69s
