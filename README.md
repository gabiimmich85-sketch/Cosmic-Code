# 🚀 COSMIC CODE

Um jogo de nave 2D estilo Arcade focado em sobrevivência, reflexos rápidos e pontuação competitiva. Desenvolvido para aplicar conceitos estruturais de Ciência da Computação, como programação orientada a objetos, consumo de APIs externas e arquitetura de componentes em engine gráfica.

 <img width="800" height="450" alt="2026-09-0921-25-24-ezgif com-video-to-gif-converter" src="https://github.com/user-attachments/assets/75bd3738-eaba-473a-87c4-d239983bd82f" />


## 🎮 Features e Mecânicas

*   **Leaderboard Global In-Game:** Integração nativa com a API do **LootLocker** para buscar e exibir o Top 10 jogadores em tempo real.
*   **Power-ups Dinâmicos:** Sistema de drop baseado em RNG onde inimigos derrotados podem fornecer Tiro Rápido (Metralhadora) ou Corações de Vida.
*   **Game Feel Aprimorado:** Sistema customizado de *Screen Shake* associado a partículas físicas (`CpuParticles2D`) e design de som de sobreposição (polifonia) para máximo impacto visual e auditivo nas explosões.
*   **IA de Perseguição:** Inimigos com lógica teleguiada que rastreiam as coordenadas do jogador, forçando movimentação constante.
*   **Controle de Sobrevivência:** Mecânica de i-frames (invencibilidade temporária baseada em tempo) após sofrer colisão, permitindo recuperação tática.

## 🛠️ Stack Tecnológico

*   **Engine:** Godot 4.x
*   **Linguagem:** C# (.NET / MSBuild)
*   **BaaS / Backend:** LootLocker 

## 🕹️ Como Jogar

*   **[Setas]:** Movimentar a nave.
*   **[Espaço]:** Disparar lasers.
*   **Objetivo:** Sobreviva, colete melhorias e destrua a maior quantidade de inimigos para registrar seu nome no ranking global!

## 💻 Como Rodar o Código Localmente

1. Clone este repositório no seu terminal:
   ```bash
