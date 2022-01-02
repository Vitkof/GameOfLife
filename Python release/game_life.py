import pygame as pg
from random import randint
from copy import deepcopy
from numba import njit
import numpy as np

pg.init()
w, h = 1200, 900
fps = 30
CELL = 3
W, H = w // CELL, h // CELL
window = pg.display.set_mode((w, h))
timer = pg.time.Clock()

next_g = np.array([[0 for X in range(W)] for Y in range(H)])
'''
# 2 медленных варианта
current = np.array([[randint(0, 1) for X in range(W)] for Y in range(H)])
current = np.array([[1 if (X + Y//2) % 4 == 0 else 0 for X in range(W)] for Y in range(H)])
'''
# 1 быстрый
current = np.array([[1 if X == W//2 or Y == H//2 else 0 for X in range(W)] for Y in range(H)])


@njit(fastmath=True)
def check_status(array, x, y):
    value = 0
    for Y in range(y - 1, y + 2):
        for X in range(x - 1, x + 2):
            value += array[Y][X]
    value -= array[y][x]

    if value == 2:     # Если клетка имеет 2 «живых» соседей, она остаётся в прежнем состоянии
        return array[y][x]
    elif value == 3:   # Если клетка имеет 3 «живых» соседей, она переходит в «живое» состояние
        return 1
    else:              # В остальных случаях -«умирает»
        return 0


@njit(fastmath=True)
def check_array(in_arr, out_arr):
    result = []
    for X in range(1, W-1):
        for Y in range(1, H-1):
            if check_status(in_arr, X, Y):
                out_arr[Y][X] = 1
                result.append((X, Y))
            else:
                out_arr[Y][X] = 0
    return out_arr, result


while True:
    window.fill(pg.Color('lightgray'))

    for event in pg.event.get():
        if event.type == pg.QUIT:
            exit()

    [pg.draw.line(window, pg.Color('darkgray'), (x, 0), (x, h)) for x in range(0, w, CELL)]
    [pg.draw.line(window, pg.Color('darkgray'), (0, y), (w, y)) for y in range(0, h, CELL)]
    next_g, res = check_array(current, next_g)
    [pg.draw.rect(window, pg.Color('black'), (x*CELL, y*CELL, CELL, CELL)) for x, y in res]
    current = deepcopy(next_g)

    pg.display.flip()
    timer.tick(fps)
    pg.display.set_caption(f"Game of Life [fps: {round(timer.get_fps(), 1)}]")
