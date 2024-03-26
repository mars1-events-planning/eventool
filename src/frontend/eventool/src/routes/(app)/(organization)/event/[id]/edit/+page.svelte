<script lang="ts">
	import { page } from '$app/stores';
	import { graphql } from '$houdini';
	import Authorized from '$lib/components/Authorized.svelte';
	import EventGuests from '$lib/components/EventGuests.svelte';
	import EventMetadata from '$lib/components/EventMetadata.svelte';
	import EventTasks from '$lib/components/EventTasks.svelte';
	import Preloader from '$lib/components/icons/Preloader.svelte';

	const eventStore = graphql(`
		query GetEventById($eventId: String!) {
			event(eventId: $eventId) {
				id
				title
				address
				description
				startAtUtc
				checklists {
					id
					title
					items {
						title
						done
					}
				}
			}
		}
	`);

	$effect.pre(() => {
		eventStore.fetch({ variables: { eventId: $page.params.id } });
	});

	let event = $derived($eventStore.data?.event!);
</script>

<Authorized>
	{#if $eventStore.fetching}
		<Preloader />
	{:else if event}
		<div class="flex flex-col gap-5 max-lg:items-center">
			<h1 class="font-semibold text-xl">
				Событие: «{event.title}»
			</h1>

			<EventMetadata {event} />
			<EventGuests />
			<EventTasks {event} />
		</div>
	{/if}
</Authorized>
