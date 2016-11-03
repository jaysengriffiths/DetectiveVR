using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Games : MonoBehaviour {

    public static string[,] game1 = {
    {"RC", "RI", "WO", "WB", "GH", "BS",  "GS", "XX",},
    {"WE", "BE", "BF", "BK", "RK", "RS", "GF", "PW",},
    {"WD", "RD", "RN", "GN", "WB", "WN", "BN", "PT",},
    {"GZ", "BC", "BO", "GB", "BA", "GA", "GC", "XX",},
    {"WJ", "WC", "GO", "GM", "RN", "RF", "BM", "PR",},
    {"RD", "BB", "RH", "BH", "WH", "WF", "RI", "PG",},
    {"GO", "WK", "WM", "RC", "GK", "BZ", "RU", "XX",},
    {"RU",   "BU", "BD",  "RB",  "RA", "WS", "WA", "PQ"},
    };

    public static string[,] game2 = {
    {"GH", "BF", "RM", "GC", "BC", "RC",  "WI", "XX",},
    {"WJ", "WO", "RB", "GS", "WA", "RK", "BI", "PG",},
    {"BH", "GO", "RI", "BS", "BM", "GK", "GF", "PW",},
    {"BA", "GD", "WD", "BU", "WB", "WC", "WE", "XX",},
    {"RH", "WH", "WS", "RU", "RN", "WF", "BA", "PQ",},
    {"RO", "GA", "BB", "GB", "GN", "WZ", "RA", "XX",},
    {"BO", "GI", "BD", "WM", "RE", "WU", "BM", "PT",},
    {"BZ",   "RZ", "RD",  "WK",  "RS", "RU", "RM", "PR"},
    };

    public static string[,] game3 = {
    {"BC", "GZ", "WF", "RE", "BH", "WK",  "RI", "XX",},
    {"RU", "GS", "BS", "GS", "GC", "BK", "RM", "PR",},
    {"GE", "GF", "BU", "RK", "RC", "WS", "GI", "PG",},
    {"BD", "WD", "WU", "GK", "BI", "WA", "BF", "PW",},
    {"BO", "RH", "RO", "GN", "RB", "GU", "RF", "XX",},
    {"GO", "GB", "WB", "BN", "WH", "WZ", "RZ", "XX",},
    {"RA", "RD", "WO", "BZ", "GH", "BA", "GA", "PQ",},
    {"WJ", "WN", "WE",  "BM",  "GM", "RS", "RN", "XX"},
    };

    public static string[,] game4 = {
    {"RU", "BN", "WK", "BK", "RK", "RZ",  "BN", "PT",},
    {"WU", "GS", "WS", "GB", "WN", "WZ", "BN", "XX",},
    {"BZ", "GH", "BZ", "GZ", "RN", "BA", "RA", "PQ",},
    {"WJ", "RH", "BB", "BM", "RB", "GK", "GM", "PR",},
    {"WO", "RE", "WE", "GU", "WB", "WC", "BC", "XX",},
    {"BO", "GF", "WD", "RD", "BI", "RM", "BF", "PW",},
    {"BE", "RC", "BU", "RF", "WA", "GC", "WI", "XX",},
    {"BD",   "GD", "GO",  "RO",  "GI", "GE", "RI", "PG"},
    };

    public static string[,] game5 = {
    {"RM", "BC", "RZ", "BU", "WU", "BH",  "GH", "XX",},
    {"GE", "WO", "BZ", "BF", "RK", "BA", "RA", "PQ",},
    {"GC", "BS", "GA", "WS", "GS", "GU", "WB", "XX",},
    {"WH", "RB", "GD", "WK", "BM", "RU", "WM", "PR",},
    {"RD", "BO", "BN", "WC", "GZ", "RH", "BI", "PG",},
    {"WJ", "GO", "RN", "RC", "GI", "RE", "GN", "PT",},
    {"WD", "GB", "WE", "BA", "BK", "BE", "WN", "XX",},
    {"BD",   "BB", "RI",  "RO",  "GK", "GF", "RF", "PW"},
    };

}
