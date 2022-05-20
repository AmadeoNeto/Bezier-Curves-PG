# Resumo: Elementos Importantes do Unity

## Conceitos Importantes:
* **GameObject:** são, a grosso modo, os objetos que compõem uma cena do jogo
* **Transform:** componente que guarda informações sobre posição, rotação e escala de um GameObject
* **Prefab**: GameObject guardado nos arquivos do jogo (não só na cena), criado para permitir "clonar" o mesmo GameObject várias vezes

## "Executadores" de código:
* **Start:** chamado quando o jogo inicia ou quando o objeto é criado (caso o jogo já esteja em execução)
* **Awake:** mesmo que o start, mas roda antes
* **Update:** chamado a cada frame
* **OnMouseDown**: chamado quando o objeto do componente é clicado
* **OnMouseDrag**: chamado quando o objeto está sendo arrastado pelo mouse