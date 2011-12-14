/// <reference path="../lib/ext/adapter/ext/ext-base.js"/>
/// <reference path="../lib/ext/ext-all-debug.js" />

Ext.ns('Ext.ux', 'Algorithm');//算法

Algorithm.basicAnalysis = function () {
    var sup = [];
    if (arguments.length == 1) {
        if (arguments[0][0] == Math.max(arguments[0][0], arguments[0][1], arguments[0][2])) {
            sup.push(1);
        } else if (arguments[0][0] == Math.min(arguments[0][0], arguments[0][1], arguments[0][2])) {
            sup.push(-1);
        } else {
            sup.push(0);
        }
        if (arguments[0][1] == Math.max(arguments[0][0], arguments[0][1], arguments[0][2])) {
            sup.push(1);
        } else if (arguments[0][1] == Math.min(arguments[0][0], arguments[0][1], arguments[0][2])) {
            sup.push(-1);
        } else {
            sup.push(0);
        }
        if (arguments[0][2] == Math.max(arguments[0][0], arguments[0][1], arguments[0][2])) {
            sup.push(1);
        } else if (arguments[0][2] == Math.min(arguments[0][0], arguments[0][1], arguments[0][2])) {
            sup.push(-1);
        } else {
            sup.push(0);
        }
        arguments[0].push(sup);
    }
    return arguments[0];
}

Algorithm.advancedAnalysis = function () {
    var sup = [];
    if (arguments.length == 1) {
        if (arguments[0][0] == Math.max(arguments[0][0], arguments[0][1], arguments[0][2])) {
            arguments[0][0] >= 0 ? sup.push([1, 1]) : sup.push([1, 0]);
        } else if (arguments[0][0] == Math.min(arguments[0][0], arguments[0][1], arguments[0][2])) {
            arguments[0][0] >= 0 ? sup.push([-1, 1]) : sup.push([-1, 0]);
        } else {
            arguments[0][0] >= 0 ? sup.push([0, 1]) : sup.push([0, 0]);
        }
        if (arguments[0][1] == Math.max(arguments[0][0], arguments[0][1], arguments[0][2])) {
            arguments[0][1] >= 0 ? sup.push([1, 1]) : sup.push([1, 0]);
        } else if (arguments[0][1] == Math.min(arguments[0][0], arguments[0][1], arguments[0][2])) {
            arguments[0][1] >= 0 ? sup.push([-1, 1]) : sup.push([-1, 0]);
        } else {
            arguments[0][1] >= 0 ? sup.push([0, 1]) : sup.push([0, 0]);
        }
        if (arguments[0][2] == Math.max(arguments[0][0], arguments[0][1], arguments[0][2])) {
            arguments[0][2] >= 0 ? sup.push([1, 1]) : sup.push([1, 0]);
        } else if (arguments[0][2] == Math.min(arguments[0][0], arguments[0][1], arguments[0][2])) {
            arguments[0][2] >= 0 ? sup.push([-1, 1]) : sup.push([-1, 0]);
        } else {
            arguments[0][2] >= 0 ? sup.push([0, 1]) : sup.push([0, 0]);
        }
        arguments[0].push(sup);
    }
    return arguments[0];
}

//按照平均凯利
Algorithm.kellyForecast = function (store) {
    var suphome = 0,supdraw = 0,supaway = 0;
    Ext.each(store.data.items, function (v, i, a) {
        if (i > 0) {
            var minvalue = Math.min(v.get('avehome'), v.get('avedraw'), v.get('aveaway'));
            var maxvalue = Math.max(v.get('avehome'), v.get('avedraw'), v.get('aveaway'));
            if (v.get('avehome') <= v.get('returnrate')) {
                suphome++
            }
            if (v.get('avedraw') <= v.get('returnrate')) {
                supdraw++
            }
            if (v.get('aveaway') <= v.get('returnrate')) {
                supaway++
            }
        }
    });
    return this.basicAnalysis([suphome, supdraw, supaway]);
};





//临场参照预测法
//以结束点位基础遍历分析
Algorithm.lastPointForecast = function (store) {
    var suphome = 0,
    supdraw = 0,
    supaway = 0;
    var end = store.getAt(store.getTotalCount() - 1);
    for (var i = 0; i < store.getTotalCount(); i++) {
        var start = store.getAt(i);
        var home = start.get("varhome") - end.get("varhome");
        var draw = start.get("vardraw") - end.get("vardraw");
        var away = start.get("varaway") - end.get("varaway");

        if (start.get("returnrate") < end.get("returnrate")) {
            if (Math.max(home, draw, away) == home) {
                suphome++
            } else if (Math.min(home, draw, away) == home) {
                suphome--
            }
            if (Math.max(home, draw, away) == draw) {
                supdraw++
            } else if (Math.min(home, draw, away) == draw) {
                supdraw--
            }
            if (Math.max(home, draw, away) == away) {
                supaway++
            } else if (Math.min(home, draw, away) == away) {
                supaway--
            }
        }
        else if (start.get("returnrate") > end.get("returnrate")) {
            if (Math.max(home, draw, away) == home) {
                suphome--
            } else if (Math.min(home, draw, away) == home) {
                suphome++
            }
            if (Math.max(home, draw, away) == draw) {
                supdraw--
            } else if (Math.min(home, draw, away) == draw) {
                supdraw++
            }
            if (Math.max(home, draw, away) == away) {
                supaway--
            } else if (Math.min(home, draw, away) == away) {
                supaway++
            }
        }
    }
    return this.basicAnalysis([suphome, supdraw, supaway]);
}
//起始参照预测法
//以起始点位基础遍历分析
Algorithm.startPointForecast = function (store) {
    var suphome = 0,
    supdraw = 0,
    supaway = 0;
    var end = store.getAt(store.getTotalCount() - 1);
    for (var i = 0; i < store.getTotalCount(); i++) {
        var start = store.getAt(i);
        var source = store.getAt(0);

        var home = start.get("varhome") - source.get("varhome");
        var draw = start.get("vardraw") - source.get("vardraw");
        var away = start.get("varaway") - source.get("varaway");
        if (start.get("returnrate") > 0) {
            if (Math.max(home, draw, away) == home) {
                suphome--
            } else if (Math.min(home, draw, away) == home) {
                suphome++
            }
            if (Math.max(home, draw, away) == draw) {
                supdraw--
            } else if (Math.max(home, draw, away) == draw) {
                supdraw++
            }
            if (Math.max(home, draw, away) == away) {
                supaway--
            } else if (Math.min(home, draw, away) == away) {
                supaway++
            }
        }
        else if (start.get("returnrate") < 0) {
            if (Math.max(home, draw, away) == home) {
                suphome++;
            } else if (Math.max(home, draw, away) == home) {
                suphome--
            }
            if (Math.max(home, draw, away) == draw) {
                supdraw++
            } else if (Math.min(home, draw, away) == draw) {
                supdraw--
            }
            if (Math.max(home, draw, away) == away) {
                supaway++
            } else if (Math.min(home, draw, away) == away) {
                supaway--
            }
        }
    }
    return this.basicAnalysis([suphome, supdraw, supaway]);
}

//方差临场变化
Algorithm.varKellyForecast = function (store) {
    var count = store.getTotalCount();
    var last1 = store.getAt(count - 2);
    var last2 = store.getAt(count - 1);
    return this.advancedAnalysis([last1.get("varhome") - last2.get("varhome"), last1.get("vardraw") - last2.get("vardraw"), last1.get("varaway") - last2.get("varaway")]);
}

//凯利临场变化
Algorithm.aveKellyForecast = function (store) {
    var count = store.getTotalCount();
    var last1 = store.getAt(count - 2);
    var last2 = store.getAt(count - 1);
    return this.advancedAnalysis([last1.get("avehome") - last2.get("avehome"), last1.get("avedraw") - last2.get("avedraw"), last1.get("aveaway") - last2.get("aveaway")]);
}

//每个点的变化
Algorithm.eachPointForecast = function (store) {
    var suphome = 0, supdraw = 0, supaway = 0;
    for (var i = 0; i < store.getTotalCount(); i++) {
        var start = store.getAt(i);
        for (var f = i + 1; f < store.getTotalCount(); f++) {
            var next = store.getAt(f + 1);
            if (!next) {
                continue;
            }
            var home = start.get("varhome") - next.get("varhome");
            var draw = start.get("vardraw") - next.get("vardraw");
            var away = start.get("varaway") - next.get("varaway");
            if (start.get("returnrate") > next.get("returnrate")) {//不看好
                if (Math.max(home, draw, away) == home) {
                    suphome--;
                } else if (Math.min(home, draw, away) == home) {
                    suphome++;
                }
                if (Math.max(home, draw, away) == draw) {
                    supdraw--;
                } else if (Math.min(home, draw, away) == draw) {
                    supdraw++;
                }
                if (Math.max(home, draw, away) == away) {
                    supaway--;
                } else if (Math.min(home, draw, away) == away) {
                    supaway++;
                }
                home >= 0 ? suphome-- : suphome++;
                draw >= 0 ? supdraw-- : supdraw++;
                away >= 0 ? supaway-- : supaway++;
            } else if (start.get("returnrate") < next.get("returnrate")) {//看好
                if (Math.max(home, draw, away) == home) {
                    suphome++;
                } else if (Math.min(home, draw, away) == home) {
                    suphome--;
                }
                if (Math.max(home, draw, away) == draw) {
                    supdraw++;
                } else if (Math.min(home, draw, away) == draw) {
                    supdraw--;
                }
                if (Math.max(home, draw, away) == away) {
                    supaway++;
                } else if (Math.min(home, draw, away) == away) {
                    supaway--;
                }
                home >= 0 ? suphome++ : suphome--;
                draw >= 0 ? supdraw++ : supdraw--;
                away >= 0 ? supaway++ : supaway--;
            }
        }

    }
    return this.basicAnalysis([suphome, supdraw, supaway]);
}