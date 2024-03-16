
export function formatDateString(dateString: string): string {
    // Parse the string into a Date object
    const date = new Date(dateString);

    // Extract the necessary components
    const hours = date.getHours().toString().padStart(2, '0');
    const minutes = date.getMinutes().toString().padStart(2, '0');
    const day = date.getDate().toString().padStart(2, '0');
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // +1 because getMonth() returns 0-11
    const year = date.getFullYear();

    // Concatenate the components in the desired format
    const formattedString = `${hours}:${minutes} ${day}.${month}.${year}`;

    return formattedString;
}