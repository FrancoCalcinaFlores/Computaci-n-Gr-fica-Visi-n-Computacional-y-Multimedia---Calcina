def validar_contrasena(contrasena: str) -> dict:

    errores = []

    caracteres_especiales = "!@#$%^&*"

    if len(contrasena) < 8:
        errores.append("La contraseña debe tener al menos 8 caracteres")

    if not any(c.isupper() for c in contrasena):
        errores.append("Debe contener al menos una letra mayúscula")

    if not any(c.islower() for c in contrasena):
        errores.append("Debe contener al menos una letra minúscula")

    if not any(c.isdigit() for c in contrasena):
        errores.append("Debe contener al menos un dígito numérico")

    if not any(c in caracteres_especiales for c in contrasena):
        errores.append("Debe contener al menos un carácter especial")

    return {
        "valida": len(errores) == 0,
        "errores": errores
    }