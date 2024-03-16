import { setSession } from '$houdini'
import type { Handle } from '@sveltejs/kit'

export const handle: Handle = async ({ event, resolve }) => {
    const token = event.cookies.get('token')
    setSession(event, { token })
    // pass the event onto the default handle
    return await resolve(event)
}
