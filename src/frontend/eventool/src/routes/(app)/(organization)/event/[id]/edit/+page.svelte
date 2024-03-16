<script lang="ts">
	import { page } from '$app/stores';
	import { graphql } from '$houdini';
	import Authorized from '$lib/components/Authorized.svelte';
	import Preloader from '$lib/components/icons/Preloader.svelte';

	const eventStore = graphql(`
		query GetEventById($eventId: String!) {
			event(eventId: $eventId) {
				id
				title
                createdAt
                changedAt
			}
		}
	`);

    $effect.pre(() => {
        eventStore.fetch({ variables: { eventId: $page.params.id } });
    });
</script>

<Authorized>
	{#if $eventStore.fetching}
		<Preloader />
	{:else}
		{$eventStore.data?.event?.title ?? 'No event found'}
	{/if}
</Authorized>
