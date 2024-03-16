<script lang="ts">
	const { ...props } = $props<{
		title: string;
		buttonClasses: string;
	}>();

	let opened = $state(false);
</script>

<button class="btn {props.buttonClasses}" onclick={() => (opened = true)}>
	<slot name="button" />
</button>

<dialog id={crypto.randomUUID()} class="modal" open={opened}>
	<div class="modal-box max-w-4xl w-auto mx-auto overflow-y-auto p-4">
		<div class="flex flex-col gap-2">
			<div class="flex flex-row justify-between items-center">
				<p>{props.title}</p>
				<form method="dialog">
					<button class="btn btn-sm btn-circle btn-ghost font-bold" onclick={() => (opened = false)}
						>✕
					</button>
				</form>
			</div>
			<div class="flex">
				<slot name="content" />
			</div>
		</div>
	</div>
	<form method="dialog" class="modal-backdrop">
		<button onclick={() => (opened = false)}>close</button>
	</form>
</dialog>
