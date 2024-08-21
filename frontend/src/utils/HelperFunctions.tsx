export function convertToSpaceSeparated(input: string): string {
    let result = "";
    for (let i = 0; i < input.length; i++) {
        const char = input[i];

        if (i > 0 && char === char.toUpperCase() && input[i - 1] !== ' ') {
            result += ' ';
        }
        
        result += char;
    }
    return result;
};