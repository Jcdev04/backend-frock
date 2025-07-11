Feature: Iniciar sesión
  Como usuario quiero poder iniciar sesión en mi cuenta creada
  Para poder usar los servicios

  Scenario: Acceso exitoso a la cuenta
    Given no existe cuenta a acceder
    When envío los datos para acceder:
      | email       | password |
      |user@test.com|user123|
    Then accedo a mi cuenta
    And el sistema retorna el id, username, role y token válidos
