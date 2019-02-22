// Date   : 19.02.2019 20:32
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;

public enum LogLevel {
    None,
    JustErrors,
    ErrorsAndWarnings,
    Everything
}

public enum LogType {
    None,
    Error,
    Warning,
    DebugMessage
}

public class DebugLogger : MonoBehaviour {

    public static DebugLogger main;
    private GameConfig gameConfig;

    void Awake() {
        main = this;
    }

    private void InitializeConfig() {
        if (gameConfig == null) {
            //gameConfig = GameManager.main.Config;
        }
    }

    private void Log(LogType logType, string message) {
        if (CanLog(logType)) {
            Debug.Log(message);
        }
    }
    private void Log(LogType logType, object message) {
        if (CanLog(logType)) {
            Debug.Log(message);
        }
    }

    private void Log(LogType logType, string message, params object[] list) {
        Log(logType, string.Format(message, list));
    }

    private bool CanLog(LogType logType) {
        InitializeConfig();
        if (gameConfig.LogLevel == LogLevel.JustErrors)  {
            return logType == LogType.Error;
        } else if (gameConfig.LogLevel == LogLevel.ErrorsAndWarnings) {
            return logType == LogType.Error || logType == LogType.Warning;
        } else if (gameConfig.LogLevel == LogLevel.Everything) {
            return true;
        }
        return false;
    }

    public void LogMessage(object message) {
        Log(LogType.DebugMessage, message);
    }

    public void LogWarning(string message) {
        Log(LogType.Warning, message);
    }
    public void LogError(string message) {
        Log(LogType.Warning, message);
    }
    public void LogMessage(string message, params object[] list) {
        Log(LogType.DebugMessage, message, list);
    }

    public void LogWarning(string message, params object[] list) {
        Log(LogType.Warning, string.Format(message, list));
    }

    public void LogError(string message, params object[] list) {
        Log(LogType.Error, message, list);
    }
}
