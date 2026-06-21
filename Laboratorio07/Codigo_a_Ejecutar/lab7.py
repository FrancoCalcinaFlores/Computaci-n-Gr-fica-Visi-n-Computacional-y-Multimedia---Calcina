import cv2
import numpy as np
import tkinter as tk
from PIL import Image, ImageTk
from tkinter import messagebox

# =========================
# CARGA DE IMÁGENES
# =========================
img1 = cv2.imread("img1.jpg")
img2 = cv2.imread("img2.jpg")
img3 = cv2.imread("img3.jpg")

if img1 is None or img2 is None or img3 is None:
    messagebox.showerror("Error", "Asegúrate de tener img1.jpg, img2.jpg e img3.jpg")
    exit()

# =========================
# REDIMENSIONAR AL TAMAÑO MAYOR
# =========================
imagenes = [img1, img2, img3]
h = max(img.shape[0] for img in imagenes)
w = max(img.shape[1] for img in imagenes)

img1 = cv2.resize(img1, (w, h))
img2 = cv2.resize(img2, (w, h))
img3 = cv2.resize(img3, (w, h))

imagen_actual = img1.copy()

# =========================
# FUNCIÓN PARA MOSTRAR IMAGEN
# =========================
def actualizar_imagen(img):
    global imagen_tk, imagen_actual
    imagen_actual = img.copy()
    img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    img_pil = Image.fromarray(img_rgb)
    img_pil = img_pil.resize((420, 300))
    imagen_tk = ImageTk.PhotoImage(img_pil)
    label_img.config(image=imagen_tk)

# =========================
# BOTONES BÁSICOS
# =========================
def mostrar_img1():
    actualizar_imagen(img1)

def mostrar_img2():
    actualizar_imagen(img2)

def mostrar_img3():
    actualizar_imagen(img3)

# =========================
# COMBINACIÓN DE CANALES
# =========================
def combinar_canales():
    b1, g1, r1 = cv2.split(img1)
    b2, g2, r2 = cv2.split(img2)
    b3, g3, r3 = cv2.split(img3)
    combinada = cv2.merge([b3, g2, r1])
    actualizar_imagen(combinada)

# =========================
# NEGATIVO Y GRIS
# =========================
def negativo():
    neg = 255 - imagen_actual
    cv2.imwrite("negativo.jpg", neg)
    actualizar_imagen(neg)

def escala_grises():
    gray = cv2.cvtColor(imagen_actual, cv2.COLOR_BGR2GRAY)
    cv2.imwrite("gris.jpg", gray)
    gray_bgr = cv2.cvtColor(gray, cv2.COLOR_GRAY2BGR)
    actualizar_imagen(gray_bgr)

# =========================
# UMBRAL BINARIO
# =========================
def umbral_binario():
    gray = cv2.cvtColor(imagen_actual, cv2.COLOR_BGR2GRAY)
    _, binaria = cv2.threshold(gray, 127, 255, cv2.THRESH_BINARY)
    bin_bgr = cv2.cvtColor(binaria, cv2.COLOR_GRAY2BGR)
    actualizar_imagen(bin_bgr)

# =========================
# CANALES DE COLOR
# =========================
def canal_rojo():
    temp = imagen_actual.copy()
    temp[:, :, 0] = 0
    temp[:, :, 1] = 0
    actualizar_imagen(temp)

def canal_verde():
    temp = imagen_actual.copy()
    temp[:, :, 0] = 0
    temp[:, :, 2] = 0
    actualizar_imagen(temp)

def canal_azul():
    temp = imagen_actual.copy()
    temp[:, :, 1] = 0
    temp[:, :, 2] = 0
    actualizar_imagen(temp)

# =========================
# DIBUJAR FIGURA + TEXTO
# =========================
def dibujar_figura_y_texto():
    win = tk.Toplevel(ventana)
    win.title("Figura con texto")

    figura = tk.StringVar(value="circulo")

    tk.Label(win, text="Selecciona la figura:").pack(pady=5)
    tk.Radiobutton(win, text="Círculo", variable=figura, value="circulo").pack()
    tk.Radiobutton(win, text="Cuadrado", variable=figura, value="cuadrado").pack()

    tk.Label(win, text="Texto dentro de la figura:").pack(pady=5)
    entrada = tk.Entry(win, width=30)
    entrada.pack(pady=5)

    def aplicar():
        texto = entrada.get()
        img = imagen_actual.copy()
        h, w, _ = img.shape

        centro = (w // 2, h // 2)

        # Dibujar figura
        if figura.get() == "circulo":
            radio = 100
            cv2.circle(img, centro, radio, (0, 255, 0), 3)
            text_size = cv2.getTextSize(texto, cv2.FONT_HERSHEY_SIMPLEX, 1, 2)[0]
            text_pos = (centro[0] - text_size[0] // 2,
                        centro[1] + text_size[1] // 2)

        else:  # cuadrado
            lado = 200
            top_left = (centro[0] - lado // 2, centro[1] - lado // 2)
            bottom_right = (centro[0] + lado // 2, centro[1] + lado // 2)
            cv2.rectangle(img, top_left, bottom_right, (0, 255, 0), 3)
            text_size = cv2.getTextSize(texto, cv2.FONT_HERSHEY_SIMPLEX, 1, 2)[0]
            text_pos = (centro[0] - text_size[0] // 2,
                        centro[1] + text_size[1] // 2)

        # Texto centrado dentro
        cv2.putText(
            img,
            texto if texto else "Objeto",
            text_pos,
            cv2.FONT_HERSHEY_SIMPLEX,
            1,
            (255, 0, 0),
            2
        )

        cv2.imwrite("imagen_figura_texto.jpg", img)
        actualizar_imagen(img)
        win.destroy()

    tk.Button(win, text="Aplicar", command=aplicar).pack(pady=10)
# =========================
# PROGRAMA DE DIBUJO INTERACTIVO

def abrir_dibujo():
    canvas = np.ones((600, 800, 3), dtype=np.uint8) * 255
    historial = []
    figura = "circle"
    inicio = None

    def mouse_event(event, x, y, flags, param):
        nonlocal inicio, canvas
        if event == cv2.EVENT_LBUTTONDOWN:
            inicio = (x, y)
            historial.append(canvas.copy())
        elif event == cv2.EVENT_LBUTTONUP and inicio:
            if figura == "circle":
                r = int(np.linalg.norm(np.array(inicio) - np.array((x, y))))
                cv2.circle(canvas, inicio, r, (0, 0, 0), 2)
            elif figura == "rectangle":
                cv2.rectangle(canvas, inicio, (x, y), (0, 0, 0), 2)
            elif figura == "line":
                cv2.line(canvas, inicio, (x, y), (0, 0, 0), 2)
            inicio = None

    cv2.namedWindow("Dibujo Interactivo")
    cv2.setMouseCallback("Dibujo Interactivo", mouse_event)

    while True:
        cv2.imshow("Dibujo Interactivo", canvas)
        k = cv2.waitKey(1)

        if k == ord('c'):
            figura = "circle"
        elif k == ord('r'):
            figura = "rectangle"
        elif k == ord('l'):
            figura = "line"
        elif k == ord('z') and historial:
            canvas = historial.pop()
        elif k == ord('s'):
            cv2.imwrite("dibujo_final.jpg", canvas)
        elif k == 27:
            break

    cv2.destroyWindow("Dibujo Interactivo")

# =========================
# INTERFAZ PRINCIPAL
# =========================
ventana = tk.Tk()
ventana.title("Laboratorio 07 - Procesamiento de Imágenes")

frame_img = tk.Frame(ventana)
frame_img.pack(side="left", padx=10, pady=10)

frame_btn = tk.Frame(ventana)
frame_btn.pack(side="right", padx=10, pady=10)

label_img = tk.Label(frame_img)
label_img.pack()

# BOTONES
tk.Button(frame_btn, text="Imagen 1", command=mostrar_img1, width=30).pack(pady=2)
tk.Button(frame_btn, text="Imagen 2", command=mostrar_img2, width=30).pack(pady=2)
tk.Button(frame_btn, text="Imagen 3", command=mostrar_img3, width=30).pack(pady=2)

tk.Button(frame_btn, text="Combinar Canales", command=combinar_canales, width=30).pack(pady=6)
tk.Button(frame_btn, text="Negativo", command=negativo, width=30).pack(pady=2)
tk.Button(frame_btn, text="Escala de Grises", command=escala_grises, width=30).pack(pady=2)
tk.Button(frame_btn, text="Umbral Binario", command=umbral_binario, width=30).pack(pady=2)

tk.Button(frame_btn, text="Canal Rojo", command=canal_rojo, width=30).pack(pady=2)
tk.Button(frame_btn, text="Canal Verde", command=canal_verde, width=30).pack(pady=2)
tk.Button(frame_btn, text="Canal Azul", command=canal_azul, width=30).pack(pady=2)

tk.Button(frame_btn, text="Dibujar Figura + Texto", command=dibujar_figura_y_texto, width=30).pack(pady=6)
tk.Button(frame_btn, text="Crear / Dibujar (Interactivo)", command=abrir_dibujo, width=30).pack(pady=6)

tk.Button(frame_btn, text="Salir", command=ventana.destroy, width=30).pack(pady=10)

mostrar_img1()
ventana.mainloop()