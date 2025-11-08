import argparse
import os
import sys
import requests
import json

# =======================================================================
# CONFIGURAÇÃO DA API (Pode ser ajustada para usar o SDK oficial do Google,
# mas o requests é mais simples para este cenário de pipeline)
# =======================================================================
API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent"
MODEL_NAME = "gemini-2.0-flash"

def get_changelog_item(api_key: str, prompt_text: str, pr_title: str, pr_body: str) -> str:
    """
    Chama a API do Gemini para gerar um item de changelog.
    """
    
    # Adiciona o contexto da PR ao prompt de geração
    context_text = f"""
    --- DADOS DA PULL REQUEST ---
    Título (Conventional Commit): {pr_title}
    Corpo da PR (Descrição Detalhada para Changelog): {pr_body}
    ---
    
    Instrução: Com base nos dados acima, gere o item do changelog seguindo rigorosamente o formato Markdown.
    """
    
    full_prompt = prompt_text + context_text

    headers = {
        "Content-Type": "application/json"
    }
    
    params = {
        "key": api_key
    }

    payload = {
        "model": MODEL_NAME,
        "contents": [
            {
                "role": "user",
                "parts": [
                    {"text": full_prompt}
                ]
            }
        ]
    }

    try:
        response = requests.post(
            API_URL, 
            headers=headers, 
            params=params, 
            json=payload, 
            timeout=30
        )
        response.raise_for_status() # Lança exceção para códigos de erro HTTP
        
        data = response.json()
        
        # Extrai o texto gerado pelo Gemini
        if 'candidates' in data and data['candidates']:
            generated_text = data['candidates'][0]['content']['parts'][0]['text']
            return generated_text.strip()
            
        return "Erro: A API do Gemini não retornou conteúdo válido."

    except requests.exceptions.RequestException as e:
        print(f"Erro na requisição à API do Gemini: {e}", file=sys.stderr)
        return "Erro: Falha na comunicação com a API."
    except Exception as e:
        print(f"Erro inesperado: {e}", file=sys.stderr)
        return "Erro: Processamento da resposta falhou."

def main():
    parser = argparse.ArgumentParser(description="Gera um item de CHANGELOG usando a API do Gemini com base nos dados de uma Pull Request.")
    parser.add_argument('--api-key', required=True, help='Chave da API do Gemini (Secret).')
    parser.add_argument('--prompt-file', required=True, help='Caminho para o arquivo TXT contendo o prompt de geração de changelog.')
    parser.add_argument('--title', required=True, help='Título da Pull Request (para tipo e escopo).')
    parser.add_argument('--body', required=True, help='Corpo/Descrição da Pull Request (para o conteúdo do item).')
    
    args = parser.parse_args()

    # Leitura do prompt do arquivo
    try:
        with open(args.prompt_file, 'r', encoding='utf-8') as f:
            prompt_text = f.read()
    except FileNotFoundError:
        print(f"Erro: Arquivo do prompt não encontrado em {args.prompt_file}", file=sys.stderr)
        sys.exit(1)

    # Chamada principal para a API
    changelog_item = get_changelog_item(
        args.api_key,
        prompt_text,
        args.title,
        args.body
    )

    # IMPRIME O RESULTADO NO STDOUT para ser capturado pelo GitHub Actions
    print(changelog_item)

if __name__ == '__main__':
    main()