Feature: Iniciar sesion
    Como usuario quiero poder inciar sesion en mi cuenta creada
    Quiero inciar sesion en mi cuenta
    Para poder usar los servicios

Scenario: Acceso exitoso de la cuenta
    Given no existe cuenta a acceder
    When envío los datos para acceder:
    | email          | password     |
    | user@email.com | password1234 |
    Then accedo a mi cuenta