

public static class Constants
{

    // Дистанция, на которой стрельба производится по позиции: позиция врага + его скорость.
    // Предполагается, что чем дальше цель - тем пуля дольше будет лететь и нужно брать с бОльшим упреждением
    public const float distancePrediction = 20f;

    // Смещение центра тела врагов на данную величину - стрелки будут целиться не в ноги существам, а выше
    public const float pivotUpForAiming = 0.2f;

    // Направление движения по оси Х должно превысить это значение, чтобы анимация развернулась по оси Х
    public const float reversEpsilon = 0.3f;

    // Имя идентификатора всех сохранений
    public const string saveRoot = "Player";

    // Пароль шифрования
    public const string savePassword = "NuclearUnderground";


}
