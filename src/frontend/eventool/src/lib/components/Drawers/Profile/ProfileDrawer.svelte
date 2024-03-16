<script>
	import ProfileDrawerSide from './ProfileDrawerSide.svelte';
	import ProfileDrawerContent from './ProfileDrawerContent.svelte';
	import Preloader from '../../icons/Preloader.svelte';
	import Drawer from '../Drawer.svelte';
	import { graphql } from '$houdini';

	const currentOrganizerDataStore = graphql(`
		query GetCurrentOrganizerData {
			organizer {
				id
				fullname
				username
			}
		}
	`);

	let fullname = $derived($currentOrganizerDataStore.data?.organizer?.fullname ?? "")
	let username = $derived($currentOrganizerDataStore.data?.organizer?.username ?? "")
</script>

{#await currentOrganizerDataStore.fetch()}
	<Drawer name="profile-drawer">
		<Preloader slot="content" />
		<Preloader slot="side" />
	</Drawer>
{:then _}
	<Drawer name="profile-drawer">
		<ProfileDrawerContent slot="content" name={fullname}/>
		<ProfileDrawerSide slot="side" initialFullname={fullname} initialUsername={username}/>
	</Drawer>
{/await}
